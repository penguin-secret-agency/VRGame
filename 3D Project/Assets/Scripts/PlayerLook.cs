using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{

    public Transform PlayerCamera;
    public Vector2 Sensitivities = new Vector2(1f, 1f);

    private Vector2 XYRotation;
    private Vector2 MouseInput = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
    }

    void OnLook(InputValue value) {
        Vector2 input = value.Get<Vector2>();
        MouseInput.x=input.x;
        MouseInput.y=input.y;
       // Debug.Log(value.Get<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {
        //bottom-top rotation
        XYRotation.x-=MouseInput.y*Sensitivities.y;
        XYRotation.x=Mathf.Clamp(XYRotation.x, -90f, 90f);
        PlayerCamera.localEulerAngles=new Vector3(XYRotation.x, 0f, 0f);
        //side-to-side rotation
        XYRotation.y+=MouseInput.x*Sensitivities.x;
        transform.eulerAngles=new Vector3(0f, XYRotation.y, 0f);
    }
}
