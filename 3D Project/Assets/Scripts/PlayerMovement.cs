using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{

    public float MoveSmoothTime = 0f;
    public float Gravity = 9.81f;
    public float WalkSpeed = 4f;
    public float RunSpeed = 8f;
    public CharacterController controller;

    private Vector3 CurrentMoveVelocity;
    private Vector3 MoveDampVelocity;
    private Vector3 playerInput = Vector3.zero;
    private bool isSprinting = false;
    private Vector3 CurrentForceVelocity;
    // Start is called before the first frame update
    void Start()
    {
        controller=GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {       
        if(playerInput.magnitude > 1f) {
            playerInput.Normalize();
        }

        Vector3 moveVector = transform.TransformDirection(playerInput);
        float CurrentSpeed = isSprinting ? RunSpeed : WalkSpeed;

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

    void OnMovement(InputValue value) {
        Vector2 axis = value.Get<Vector2>();
        playerInput.x=axis.x;
        playerInput.z=axis.y;
    }

    void OnSprint(InputValue value) {
        isSprinting=value.isPressed;
    }
}
