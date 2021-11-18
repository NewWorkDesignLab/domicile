using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class EnvironmentScene : Singleton<EnvironmentScene>
{
    [Scene] public string offlineScene;

    public void LoadSceneOffline()
    {
        SceneManager.LoadScene(offlineScene);
    }
}
