using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class OfflineScene : MonoBehaviour
{
    [Scene] public string onlineScene;
    [Scene] public string offlineScene;

    void Start()
    {
#if UNITY_STANDALONE_LINUX
        LoadSceneOnline();
#endif
    }

    public void LoadSceneOnline()
    {
        SceneManager.LoadScene(onlineScene);
    }

    public void LoadSceneOffline()
    {
        SceneManager.LoadScene(offlineScene);
    }
}
