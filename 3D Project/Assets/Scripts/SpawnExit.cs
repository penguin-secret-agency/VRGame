using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class SpawnExit : MonoBehaviour
{
    public XRGrabInteractable interactable;
    public DissolverManager exitDissolverManager;
    public Light lightSource;
    // Start is called before the first frame update
    void Start()
    {
        interactable.selectEntered.AddListener((i) => {
            if(interactable.enabled) {
                StartCoroutine(undoDissolving());
            }
        });
    }


    public IEnumerator undoDissolving() {
        yield return exitDissolverManager.undoDissolving();
        lightSource.enabled=true;
    }
}
