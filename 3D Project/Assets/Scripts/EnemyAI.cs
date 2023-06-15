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
    public float attackingDistance = 0.5f;
    private float walkingSpeed = 1.5f;
    private float runningSpeed = 1f;
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
    //Decides on which state the enemy is
    private void stateDecision() {
        if(!fieldOfView) {
            return;
        }
        if(fieldOfView.canSeePlayer) {
            isTrackingPlayer=true;
            bool isClose = agent.remainingDistance<attackingDistance;
            if(isClose) {
                state=ENEMY_STATE.ATTACK;
            } else {
                state=ENEMY_STATE.CHASE;
            }
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
                if(animator) {
                    animator.SetBool("startWalking", true);
                    animator.SetFloat("Speed", walkingSpeed);
                }
                bool canGoToPosition = !agent.pathPending&&agent.remainingDistance<agent.stoppingDistance;
                if(canGoToPosition) {
                    goToNextPoint();
                }
                break;
            case ENEMY_STATE.TRACKING:
                if(animator) {
                    animator.SetFloat("Speed", runningSpeed);
                }               
                Vector3 lastKnownPosition = fieldOfView.directionToTargetRef;
                agent.SetDestination(lastKnownPosition);
                bool stopped = agent.remainingDistance<0.01f;
                if(stopped) {
                    isTrackingPlayer=false;
                }
                break;
            case ENEMY_STATE.CHASE:
                if(animator) {
                    animator.SetFloat("Speed", runningSpeed);
                }
                Vector3 targetPosition = fieldOfView.targetRef.transform.position;
                agent.SetDestination(targetPosition);
                break;
            case ENEMY_STATE.ATTACK:
                //    animator.SetFloat("Speed", attackSpeed);
                break;
            case ENEMY_STATE.IDLE:
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
}
