using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class OfflinePlayer : MonoBehaviour
{
    public BasePlayer player;

    void Start()
    {
        if (NetworkServer.active || NetworkClient.active)
        {
            gameObject.SetActive(false);
        }
        else
        {
            player.SetupLocalPlayer();
            LobbyUIManager.instance.DisableUI();
            SceneSettingHelper.DisplayVR();
        }
    }
}
