using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using Mirror;

public class GeneralNetworkHelper : MonoBehaviour
{
    public GeneralNetworkHelperCondition condition;
    public GeneralNetworkHelperComparision comparision;

    public OnlineState valueOnlineState;
    public RoomCount valueRoomCount;
    public TextureDifficulty valueTextureDifficulty;
    public PlayerRole valuePlayerRole;

    public UnityEvent evntSuccess;

    void Start()
    {
        switch (condition)
        {
            case GeneralNetworkHelperCondition.onlineState:
                bool currentOnlineStatus = NetworkServer.active || NetworkClient.active;
                bool targetOnlineStatus = valueOnlineState == OnlineState.online;
                if ((currentOnlineStatus == targetOnlineStatus && comparision == GeneralNetworkHelperComparision.equals) ||
                    (currentOnlineStatus != targetOnlineStatus && comparision == GeneralNetworkHelperComparision.notEquals))
                    evntSuccess?.Invoke();
                break;

            case GeneralNetworkHelperCondition.scenarioRoomCount:
                if (NetworkServer.active || NetworkClient.active)
                    StartCoroutine(CheckRooms());
                break;

            case GeneralNetworkHelperCondition.scenarioTextureDifficulty:
                if (NetworkServer.active || NetworkClient.active)
                    StartCoroutine(CheckTexture());
                break;

            case GeneralNetworkHelperCondition.playerRole:
                if (NetworkServer.active || NetworkClient.active)
                    StartCoroutine(CheckPlayerRole(true));
                break;
        }
    }

    public void TestConsole(string text) {
        Debug.LogError(text);
    }

    private IEnumerator CheckRooms()
    {
        while (OnlinePlayer.scenario == null)
            yield return null;

        if ((OnlinePlayer.scenario.rooms == valueRoomCount && comparision == GeneralNetworkHelperComparision.equals) ||
            (OnlinePlayer.scenario.rooms != valueRoomCount && comparision == GeneralNetworkHelperComparision.notEquals))
            evntSuccess?.Invoke();
    }

    private IEnumerator CheckTexture()
    {
        while (OnlinePlayer.scenario == null)
            yield return null;

        if ((OnlinePlayer.scenario.textures == valueTextureDifficulty && comparision == GeneralNetworkHelperComparision.equals) ||
            (OnlinePlayer.scenario.textures != valueTextureDifficulty && comparision == GeneralNetworkHelperComparision.notEquals))
            evntSuccess?.Invoke();
    }

    private IEnumerator CheckPlayerRole(bool subscribeEvent = false)
    {
        while (OnlinePlayer.localPlayer == null)
            yield return null;

        if (subscribeEvent)
            OnlinePlayer.localPlayer.OnRoleChanged += OnRoleChanged;

        if ((OnlinePlayer.localPlayer.playerRole == valuePlayerRole && comparision == GeneralNetworkHelperComparision.equals) ||
            (OnlinePlayer.localPlayer.playerRole != valuePlayerRole && comparision == GeneralNetworkHelperComparision.notEquals))
            evntSuccess?.Invoke();
    }

    void OnRoleChanged(PlayerRole newRole)
    {
        StartCoroutine(CheckPlayerRole());
    }
}


public enum GeneralNetworkHelperCondition {
    onlineState, // if is online or offline
    scenarioRoomCount, // depending on room setting
    scenarioTextureDifficulty, // depending on texture setting
    playerRole // depending on online player role
}


public enum GeneralNetworkHelperComparision {
    equals,
    notEquals
}