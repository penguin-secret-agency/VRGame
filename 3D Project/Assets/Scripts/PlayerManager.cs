using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerManager : MonoBehaviour
{
    [Header("First Person")]
    public PlayerLook playerLook;
    public PlayerMovement playerMovement;
    [Header("VR")]
    public ActionBasedContinuousMoveProvider moveProvider;
    [Header("Gameplay")]
    public HealthSystem healthSystem;
    public GameManager gameManager;
    public Transform respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        if(healthSystem != null) {            
            healthSystem.onDeath.AddListener(killPlayer);
        }
    }

    public void stopMovement() {
        if(moveProvider!=null) {
            moveProvider.moveSpeed=0f;
        }
        if(playerMovement!=null) {
            playerMovement.enabled=false;
        }
        if(playerLook!=null) {
            playerLook.enabled=false;
        }
    }

    public void killPlayer() {
        if(gameManager != null) {
            gameManager.GameOver();
        }
        stopMovement();
    }
}
