using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyPlayerUI : MonoBehaviour
{
    public TMP_Text nameField;
    public GameObject readyIcon;
    public GameObject notReadyIcon;

    DomicileNetworkRoomPlayer roomPlayer;
    string latestName;
    PlayerRole latestRole;

    public void SetPlayer(DomicileNetworkRoomPlayer _player)
    {
        roomPlayer = _player;
        // subscribe to the events raised by SyncVar Hooks on the Player object
        roomPlayer.OnNameChanged += OnNameChanged;
        roomPlayer.OnRoleChanged += OnRoleChanged;
        roomPlayer.OnReadyStateChanged += OnReadyStateChanged;
    }
    
    void OnDisable()
    {
        roomPlayer.OnNameChanged -= OnNameChanged;
        roomPlayer.OnReadyStateChanged -= OnReadyStateChanged;
        roomPlayer.OnRoleChanged -= OnRoleChanged;
    }

    private void UpdateDisplayedName()
    {
        nameField.text = System.String.Format("{0} ({1})", latestName, TextGenerator.GenerateRoleText(latestRole));
    }

    void OnNameChanged(string newName)
    {
        latestName = newName;
        UpdateDisplayedName();
    }

    void OnReadyStateChanged(bool newState)
    {
        readyIcon.SetActive(newState);
        notReadyIcon.SetActive(!newState);
    }
    
    void OnRoleChanged(PlayerRole newRole)
    {
        latestRole = newRole;
        UpdateDisplayedName();
    }
}
