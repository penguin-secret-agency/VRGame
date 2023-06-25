using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitManager : MonoBehaviour
{
    public DissolverManager exitDissolverManager;
    // Start is called before the first frame update

    private void OnTriggerStay(Collider other) {
        WinGame(other);
    }

    private void OnTriggerEnter(Collider other) {
        WinGame(other);
    }

    public void WinGame(Collider other) {
        if(!other.gameObject.CompareTag("Player")) {
            return;
        }
        if(exitDissolverManager.getDissolverAmount()<0.1f) {
            GameObject player = other.gameObject;
            PlayerManager PlayerManager = player.GetComponent<PlayerManager>();
            if(PlayerManager.gameManager.winUI.activeInHierarchy) {
                return;
            }
            PlayerManager.stopMovement();
            PlayerManager.gameObject.transform.position=PlayerManager.respawnPoint.position;
            PlayerManager.gameManager.WinGame();
            StartCoroutine(fixPadding(PlayerManager.gameManager.winUI));
        }
    }

    public IEnumerator fixPadding(GameObject ui) {
        yield return new WaitForSeconds(1);
        ui.GetComponent<VerticalLayoutGroup>().spacing+=0.1f;
    }
}
