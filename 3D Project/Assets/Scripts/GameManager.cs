using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject winUI;
    public bool isVR = false;
    // Start is called before the first frame update
    void Start()
    {
        if(isVR) {
            GetComponent<Canvas>().renderMode=RenderMode.WorldSpace;
        }
        Cursor.visible=false;
        Cursor.lockState=CursorLockMode.Locked;
    }

    public void MapScene() {
        SceneManager.LoadScene("MapScene", LoadSceneMode.Single);
    }

    public void MenuScene() {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

    public void GameOver() {
        if(winUI.activeInHierarchy) {
            return;
        }
        gameOverUI.SetActive(true);
        if(!isVR) {
            Cursor.visible=true;
            Cursor.lockState=CursorLockMode.None;
        }        
    }
    public void WinGame() {
        if(gameOverUI.activeInHierarchy) {
            return;
        }
        winUI.SetActive(true);
        if(!isVR) {
            Cursor.visible=true;
            Cursor.lockState=CursorLockMode.None;
        }
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if(!isVR) {
            Cursor.visible=false;
            Cursor.lockState=CursorLockMode.Locked;
        }
        
    }

    public void QuitGame() {
        Application.Quit();
    }
}

