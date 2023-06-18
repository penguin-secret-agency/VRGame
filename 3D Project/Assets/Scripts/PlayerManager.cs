using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerLook playerLook;
    public PlayerMovement playerMovement;
    public HealthSystem healthSystem;
    public GameManagerScript gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        if(healthSystem != null) {
            healthSystem.onDeath.AddListener(killPlayer);
        }
    }

    public void killPlayer() {
        if(gameManagerScript != null) {
            gameManagerScript.GameOver();
        }        
        if(playerMovement!=null) {
            playerMovement.enabled=false;
        }
        if(playerLook!=null) {
            playerLook.enabled=false;
        }
    }
}
