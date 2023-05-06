using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float MoveSmoothTime = 0f;
    public float Gravity = 9.81f;
    public float WalkSpeed = 4f;
    public float RunSpeed = 8f;

    private CharacterController controller;
    private Vector3 CurrentMoveVelocity;
    private Vector3 MoveDampVelocity;

    private Vector3 CurrentForceVelocity;
    // Start is called before the first frame update
    void Start()
    {
        controller=GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerInput = new Vector3 {
            x=Input.GetAxis("Horizontal"),
            y=0,
            z=Input.GetAxis("Vertical")
        };

        if(playerInput.magnitude > 1f) {
            playerInput.Normalize();
        }

        Vector3 moveVector = transform.TransformDirection(playerInput);
        float CurrentSpeed = Input.GetKey(KeyCode.LeftShift) ? RunSpeed : WalkSpeed;

        CurrentMoveVelocity=Vector3.SmoothDamp(CurrentMoveVelocity, moveVector*CurrentSpeed, ref MoveDampVelocity, MoveSmoothTime);
        controller.Move(CurrentMoveVelocity*Time.deltaTime);

        Ray groundCheckRay = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(groundCheckRay, 2f)) {
            CurrentForceVelocity.y = -2f;
        } else {
            CurrentForceVelocity.y-=Gravity*Time.deltaTime;
        }
        controller.Move(CurrentForceVelocity*Time.deltaTime);
    }
}
