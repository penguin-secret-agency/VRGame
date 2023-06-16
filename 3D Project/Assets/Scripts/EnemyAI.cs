using System.Collections;
using System.Collections.Generic;
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
    
    private Animator animator;
    public NavMeshAgent agent;
    public FieldOfView fieldOfView;
    private bool isTrackingPlayer = false;
    private float walkingSpeed = 1.3f;
    private float runningSpeed = 4.5f;
    private ENEMY_STATE state = ENEMY_STATE.IDLE;
    //Patrol
    public Transform[] points;
    private int currentPoint = 0;
    private void Start() {
        animator=GetComponent<Animator>();
        agent.stoppingDistance=0.6f;
        //Patrol
        agent.autoBraking = false;
    }

    private void Update() {            
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
                //if(animator) {
                //    animator.SetBool("startWalking", true);
                //    animator.SetFloat("Speed", walkingSpeed);
                //}
                agent.speed=walkingSpeed;
                bool canGoToPosition = !agent.pathPending&&agent.remainingDistance<agent.stoppingDistance;
                if(canGoToPosition) {
                    goToNextPoint();
                }
                break;
            case ENEMY_STATE.TRACKING:
                //if(animator) {
                //    animator.SetFloat("Speed", runningSpeed);
                //}           
                agent.speed=runningSpeed;
                Vector3 lastKnownPosition = fieldOfView.directionToTargetRef;
                agent.SetDestination(lastKnownPosition);
                bool stopped = agent.remainingDistance<0.01f;
                if(stopped) {
                    isTrackingPlayer=false;
                }
                break;
            case ENEMY_STATE.CHASE:
                //if(animator) {
                //    animator.SetFloat("Speed", runningSpeed);
                //}
                agent.speed=runningSpeed;
                Vector3 targetPosition = fieldOfView.targetRef.transform.position;
                agent.SetDestination(targetPosition);
                break;
            case ENEMY_STATE.ATTACK:
                state=ENEMY_STATE.PATROL;
                break;
            case ENEMY_STATE.IDLE:
                state=ENEMY_STATE.PATROL;
                break;
        }
    }

    private void goToNextPoint() {
        // Returns if no points have been set up
        if (points.Length == 0){
            return;
        }

        agent.SetDestination(points[currentPoint].position);

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        currentPoint = (currentPoint + 1) % points.Length;
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
