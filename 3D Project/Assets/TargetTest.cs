using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTest : MonoBehaviour
{
    private float startX;
    public float distanceX = 4f;
    public float increment = 0.005f;
    private bool moveRight = true;

    private void Start() {
        startX = transform.position.x;
    }
    private void Update() {
        if(moveRight) {
            transform.position=new Vector3(transform.position.x+increment, transform.position.y, transform.position.z);
            if(transform.position.x>=startX + distanceX) {
                moveRight=false;
            }
        } else {
            transform.position=new Vector3(transform.position.x-increment, transform.position.y, transform.position.z);
            if(transform.position.x<=startX-distanceX) {
                moveRight=true;
            }
        }
    }
}
