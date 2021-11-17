using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyPlayerUI : MonoBehaviour
{
    public TMP_Text nameField;
    public GameObject readyIcon;
    public GameObject notReadyIcon;

    OnlinePlayer onlinePlayer;
    PlayerRole latestRole;

    public void SetPlayer(OnlinePlayer _player)
    {
        onlinePlayer = _player;

        // subscribe to the events raised by SyncVar Hooks on the Player object
        onlinePlayer.OnRoleChanged += OnRoleChanged;
        onlinePlayer.OnReadyStateChanged += OnReadyStateChanged;
    }
    
    void OnDisable()
    {
        onlinePlayer.OnReadyStateChanged -= OnReadyStateChanged;
        onlinePlayer.OnRoleChanged -= OnRoleChanged;
    }

    private void UpdateDisplayedName()
    {
        nameField.text = System.String.Format("{0} ({1})", onlinePlayer.playerName, TextGenerator.GenerateRoleText(latestRole));
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
