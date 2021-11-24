using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class EnvironmentScene : Singleton<EnvironmentScene>
{
    [Scene] public string offlineScene;

    public void LoadSceneOffline()
    {
        SceneSettingHelper.Display2D();
        SceneManager.LoadScene(offlineScene);
    }

    public void SetupPlayerVisabillity()
    {
        OnlinePlayer.localPlayer.SetupClientVisabillity();
    }
}
