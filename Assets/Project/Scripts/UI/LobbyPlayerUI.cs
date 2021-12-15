using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyPlayerUI : MonoBehaviour
{
    public TMP_Text nameField;
    public GameObject readyIcon;
    public GameObject notReadyIcon;
    public GameObject readyIconLocalPlayer;
    public GameObject notReadyIconLocalPlayer;

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
        string text = System.String.Format("{0} ({1})", onlinePlayer.playerName, TextGenerator.GenerateRoleText(latestRole));
        if (onlinePlayer.isLocalPlayer)
            text = $"<b>{text}</b>";
        nameField.text = text;
    }

    void OnReadyStateChanged(bool newState)
    {
        readyIcon.SetActive(newState && !onlinePlayer.isLocalPlayer);
        notReadyIcon.SetActive(!newState && !onlinePlayer.isLocalPlayer);
        readyIconLocalPlayer.SetActive(newState && onlinePlayer.isLocalPlayer);
        notReadyIconLocalPlayer.SetActive(!newState && onlinePlayer.isLocalPlayer);
    }
    
    void OnRoleChanged(PlayerRole newRole)
    {
        latestRole = newRole;
        UpdateDisplayedName();
    }
}
