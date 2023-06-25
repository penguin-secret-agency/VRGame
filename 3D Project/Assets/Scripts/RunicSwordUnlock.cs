using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class RunicSwordUnlock : MonoBehaviour
{
    public XRGrabInteractable swordInteractable;
    public Rigidbody swordRigidBody;
    public XRGrabInteractable[] lockedInteractables;
    // Start is called before the first frame update
    void Start()
    {
        swordRigidBody=GetComponent<Rigidbody>();
        swordInteractable =GetComponent<XRGrabInteractable>();
        swordInteractable.selectEntered.AddListener((interactor) => {
            swordRigidBody.isKinematic=false;
            foreach(XRGrabInteractable interactable in lockedInteractables) {
                interactable.enabled=true;
            }
        });
        swordInteractable.selectExited.AddListener((interactor) => {
            swordRigidBody.isKinematic=false;
        });
    }

}
