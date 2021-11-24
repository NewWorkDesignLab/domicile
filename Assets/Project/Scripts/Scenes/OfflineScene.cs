using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class OfflineScene : Singleton<OfflineScene>
{
    [Scene] public string onlineScene;
    [Scene] public string offlineScene;

    void Start()
    {
        #if UNITY_STANDALONE_LINUX
        LoadSceneOnline();
        #else
        
        if (SessionInstance.instance.ShouldReconnect()) {
            OfflineUIManager.instance.HideAll();
            OfflineUIManager.instance.reconnectGroup.SetActive(true);
        } else {
            OfflineUIManager.instance.HideAll();
            OfflineUIManager.instance.mainLandingGroup.SetActive(true);
        }

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
