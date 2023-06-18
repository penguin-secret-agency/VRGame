using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    // Start is called before the first frame update
    void Start()
    {
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
        gameOverUI.SetActive(true);
        Cursor.visible=true;
        Cursor.lockState=CursorLockMode.None;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Cursor.visible=false;
        Cursor.lockState=CursorLockMode.Locked;
    }

    public void QuitGame() {
        Application.Quit();
    }
}

