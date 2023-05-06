using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolChaseAttack : MonoBehaviour
{
    public float patrolSpeed = 1.0f;
    public float chaseSpeed = 2.0f;
    public float attackSpeed = 3.0f;

    private Animator animator;
    private NavMeshAgent agent;
    private Transform target;
    private Vector3 movementDirection;

    private void Start() {
        animator=GetComponent<Animator>();
        agent=GetComponent<NavMeshAgent>();
        target=GameObject.FindGameObjectWithTag("Player").transform; // Replace "Player" with the tag of your target object
    }

    private void FixedUpdate() {
        // Calculate the distance to the target
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Set the speed parameter of the animator based on the distance to the target
        if(distanceToTarget>10.0f) {
            animator.SetFloat("Speed", patrolSpeed);
        } else if(distanceToTarget>5.0f) {
            animator.SetFloat("Speed", chaseSpeed);
        } else {
            animator.SetFloat("Speed", attackSpeed);
        }

        // Calculate movement direction
        Vector3 targetDirection = target.position-transform.position;
        targetDirection.y=0;
        movementDirection=Vector3.Lerp(movementDirection, targetDirection.normalized, Time.deltaTime*10f);

        // Set the blend tree parameters based on the movement direction
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.z);

        // Set the destination of the NavMeshAgent
        Vector3 destination = transform.position+movementDirection*agent.speed*Time.deltaTime;
        NavMeshHit hit;
        NavMesh.SamplePosition(destination, out hit, 1.0f, NavMesh.AllAreas);
        agent.SetDestination(hit.position);
    }
}
