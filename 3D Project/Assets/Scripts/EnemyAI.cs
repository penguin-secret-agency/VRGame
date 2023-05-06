using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    private Animator animator;
    private NavMeshAgent agent;
    private Transform target;
    private Vector3 movementDirection;
    public float fieldOfVisionAngle = 60.0f;
    private void Start() {
        animator=GetComponent<Animator>();
        agent=GetComponent<NavMeshAgent>();
        target=GameObject.FindGameObjectWithTag("Player").transform; // Replace "Player" with the tag of your target object
    }

    private void FixedUpdate() {
        // Calculate the distance to the target
        if(isPlayerOnSight()) { // Player on sight?
            // Chase it!
            float distanceToTarget = Vector3.Distance(agent.transform.position, target.position);
            // add "refreshable count down" to deactivate a "lost player track" behavior
        } else { // Player is not on sight
            // continue patrol behavior
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

    private bool isPlayerOnSight() {
        Vector3 distanceToTarget = target.position-transform.position;
        bool a = fieldOfVisionAngle > Vector3.Angle(transform.forward, distanceToTarget);
        Debug.Log(a);
        return distanceToTarget.magnitude < 100 && a;
    }
}
