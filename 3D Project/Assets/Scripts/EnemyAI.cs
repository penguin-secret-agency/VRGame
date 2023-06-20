using System.Collections;
using UnityEngine;
using UnityEngine.AI;
enum ENEMY_STATE {
    PATROL,
    CHASE,
    ATTACK,
    IDLE,
    TRACKING
}
public class EnemyAI : MonoBehaviour {
    
    public Animator animator;
    public NavMeshAgent agent;
    public FieldOfView fieldOfView;
    public HealthSystem enemyHealthSystem;
    private bool isTrackingPlayer = false;
    private float walkingSpeed = 1.3f;
    private float runningSpeed = 4.5f;
    private bool hasReachedDestination = true;
    private ENEMY_STATE state = ENEMY_STATE.IDLE;
    //Patrol
    public Transform[] points;
    private int currentPoint = 0;
    private void Start() {
        enemyHealthSystem=GetComponent<HealthSystem>();
        if(enemyHealthSystem != null) {
            enemyHealthSystem.onDeath.AddListener(killEnemy);
        }
        animator=GetComponent<Animator>();
    }

    private void killEnemy() {
        Destroy(gameObject);        
    }

    private void Update() {
        hasReachedDestination = agent.remainingDistance<agent.stoppingDistance;
        stateDecision();
        stateBehavior();
    }

    //Decides on which state the enemy is
    private void stateDecision() {
        if(!fieldOfView) {
            return;
        }
        GameObject Player = fieldOfView.targetRef;
        HealthSystem PlayerHealthSystem = Player ? Player.GetComponent<HealthSystem>() : null;
        bool isAlive = PlayerHealthSystem ? PlayerHealthSystem.health > 0 : true;
        if(fieldOfView.canSeePlayer && isAlive) {
            isTrackingPlayer=true;
            state=ENEMY_STATE.CHASE;        
        } else if(isTrackingPlayer) {
            state=ENEMY_STATE.TRACKING;
        } else {
            state=ENEMY_STATE.PATROL;
        }
    }

    //Decides what each state does
    private void stateBehavior() {
        switch(state) {
            case ENEMY_STATE.PATROL:
                bool canGoToPosition = !agent.pathPending&&agent.remainingDistance<agent.stoppingDistance&&!agent.isStopped;
                if(canGoToPosition) {
                    StartCoroutine(goToNextPoint(walkingSpeed));
                }
                break;
            case ENEMY_STATE.TRACKING:
                Vector3 lastKnownPosition = fieldOfView.directionToTargetRef;
                goToDestination(lastKnownPosition, runningSpeed);
                if(hasReachedDestination) {
                    isTrackingPlayer=false;
                }
                break;
            case ENEMY_STATE.CHASE:
                Vector3 targetPosition = fieldOfView.targetRef.transform.position;
                goToDestination(targetPosition, runningSpeed);
                break;
            case ENEMY_STATE.ATTACK:
                state=ENEMY_STATE.PATROL;
                break;
            case ENEMY_STATE.IDLE:
                state=ENEMY_STATE.PATROL;
                break;
        }
    }

    private IEnumerator goToNextPoint(float speed) {
        // Returns if no points have been set up
        if (points.Length == 0){
            yield return null;
        }
        agent.isStopped=true;
        animator.SetFloat("Velocity", 0f);
        yield return new WaitForSeconds(5);
        goToDestination(points[currentPoint].position, speed);

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        currentPoint = (currentPoint + 1) % points.Length;
    }

    private void goToDestination(Vector3 destination, float speed) {
        if(hasReachedDestination) {
            animator.SetFloat("Velocity", 0f);
            return;
        }
        if(speed<=walkingSpeed) {
            animator.SetFloat("Velocity", 0.06f);
        }
        if(speed>=runningSpeed) {
            animator.SetFloat("Velocity", 1f);
        }
        agent.speed=speed;
        agent.isStopped=false;
        agent.SetDestination(destination);              
    }

    void OnTriggerEnter(Collider other) {
        bool isPlayer = other.gameObject.CompareTag("Player");
        if(isPlayer&&state==ENEMY_STATE.CHASE) {
            GameObject player = other.gameObject;
            HealthSystem PlayerHealthSystem = player.GetComponent<HealthSystem>();
            PlayerHealthSystem.decreasedHealth(PlayerHealthSystem.health);
        }
    }
}
