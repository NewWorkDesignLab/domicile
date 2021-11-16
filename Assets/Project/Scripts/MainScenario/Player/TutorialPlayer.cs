using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TutorialPlayer : MonoBehaviour
{
    public OfflinePlayer player;

    void Start()
    {
        if (NetworkServer.active || NetworkClient.active)
        {
            gameObject.SetActive(false);
        }
        else
        {
            player.SetupLocalPlayer();
        }
    }
}
