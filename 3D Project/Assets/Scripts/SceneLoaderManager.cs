using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public string sceneName = "MapScene";

    public void StartScene(){
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
