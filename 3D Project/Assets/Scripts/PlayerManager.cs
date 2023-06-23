using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public PlayerLook playerLook;
    public PlayerMovement playerMovement;
    public HealthSystem healthSystem;
    public LightDamage lightDamage;
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
            Debug.Log("Mostra tela");
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
