using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public Transform PlayerCamera;
    public Vector2 Sensitivities = new Vector2(4f, 4f);

    private Vector2 XYRotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MouseInput = new Vector2 {
            x=Input.GetAxis("Mouse X"),
            y=Input.GetAxis("Mouse Y")
        };
        //bottom-top rotation
        XYRotation.x-=MouseInput.y*Sensitivities.y;
        XYRotation.x=Mathf.Clamp(XYRotation.x, -90f, 90f);
        PlayerCamera.localEulerAngles=new Vector3(XYRotation.x, 0f, 0f);
        //side-to-side rotation
        XYRotation.y+=MouseInput.x*Sensitivities.x;
        transform.eulerAngles=new Vector3(0f, XYRotation.y, 0f);        
    }
}
