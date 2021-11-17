using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRequiresAllPlayersReady : MonoBehaviour
{
    public Button button;
    public bool requiredState = true;

    void OnEnable()
    {
        SetInteraction(false);
        StartCoroutine(InitHook());
    }

    void OnDisable()
    {
        if (OnlinePlayer.localPlayer != null) OnlinePlayer.localPlayer.OnRoomReadyStateChanged -= SetInteraction;
    }

    private IEnumerator InitHook()
    {
        while (OnlinePlayer.localPlayer == null)
            yield return null;
            
        SetInteraction(OnlinePlayer.localPlayer.roomReadyState);
        OnlinePlayer.localPlayer.OnRoomReadyStateChanged += SetInteraction;
    }

    private void SetInteraction(bool value)
    {
        button.interactable = value == requiredState;
    }
}
