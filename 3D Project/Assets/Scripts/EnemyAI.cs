using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
enum ENEMY_STATE {
    PATROL,
    CHASE,
    ATTACK,
    IDLE
}
public class EnemyAI : MonoBehaviour {
    
    private Animator animator;
    private NavMeshAgent agent;
    private Transform target;
    private Vector3 movementDirection;
    public float fieldOfVisionAngle = 50.0f;
    public FieldOfView fieldOfView;
    private ENEMY_STATE state = ENEMY_STATE.IDLE;
    //Patrol
    public Transform[] points;
    private int currentPoint = 0;
    private void Start() {
        animator=GetComponent<Animator>();
        agent=GetComponent<NavMeshAgent>();
        target=GameObject.FindGameObjectWithTag("Player").transform; // Replace "Player" with the tag of your target object
        //Patrol
        agent.autoBraking = false;
        goToNextPoint();
    }

    private void Update() {
        // Calculate the distance to the target
        if(fieldOfView.canSeePlayer) {
            //if(isClose){
            //    state = ENEMY_STATE.ATTACK;
            //}else{
            //    state = ENEMY_STATE.CHASE;
            //}
            float distanceToTarget = Vector3.Distance(agent.transform.position, target.position);
            // add "refreshable count down" to deactivate a "lost player track" behavior
        } else {
            state = ENEMY_STATE.PATROL;
        }

        switch(state){
            case ENEMY_STATE.PATROL:
                bool canGoToNextPoint = !agent.pathPending && agent.remainingDistance < 0.5f;
                if (canGoToNextPoint){
                    goToNextPoint();
                }
                break;
            case ENEMY_STATE.CHASE:
                break;
            case ENEMY_STATE.ATTACK:
                break;
            case ENEMY_STATE.IDLE:
                break;
        }

        // Set the speed parameter of the animator based on the distance to the target
        //if(distanceToTarget>10.0f) {
        //    animator.SetFloat("Speed", patrolSpeed);
        //} else if(distanceToTarget>5.0f) {
        //    animator.SetFloat("Speed", chaseSpeed);
        //} else {
        //    animator.SetFloat("Speed", attackSpeed);
        //}

        // Calculate movement direction
        //Vector3 targetDirection = target.position-transform.position;
        //targetDirection.y=0;
        //movementDirection=Vector3.Lerp(movementDirection, targetDirection.normalized, Time.deltaTime*10f);

        // Set the blend tree parameters based on the movement direction
        //animator.SetFloat("Horizontal", movementDirection.x);
        //animator.SetFloat("Vertical", movementDirection.z);


    }

    void goToNextPoint() {
        // Returns if no points have been set up
        if (points.Length == 0){
            return;
        }

        // Set the agent to go to the currently selected destination.
        agent.SetDestination(points[currentPoint].position);

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        currentPoint = (currentPoint + 1) % points.Length;
    }
}
