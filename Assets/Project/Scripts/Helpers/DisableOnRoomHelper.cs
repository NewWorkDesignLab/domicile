using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DisableOnRoomHelper : MonoBehaviour
{
    public RoomCount onlyShowOnRoomSetting;

    void Start()
    {
        if (NetworkServer.active || NetworkClient.active)
            StartCoroutine(CheckRooms());
        else
            gameObject.SetActive(false);
    }
    
    private IEnumerator CheckRooms()
    {
        while (OnlinePlayer.scenario == null)
            yield return null;

        bool show = OnlinePlayer.scenario.rooms == onlyShowOnRoomSetting;
        gameObject.SetActive(show);
    }
}
