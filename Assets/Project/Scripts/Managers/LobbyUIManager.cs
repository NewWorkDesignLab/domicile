using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyUIManager : Singleton<LobbyUIManager>
{
    [Header("Lobby Prefabs")]
    public GameObject lobbyPlayerUIParent;
    public GameObject canvasGameObject;

    [Header("Lobby UI")]
    public GameObject lobbyGroup;
    public Button readyButton;
    public Button startButton;

    [Header("Groups")]
    public GameObject loadingGroup;
    public List<GameObject> allObjects;

    public void Start()
    {
        HideAll();
        loadingGroup.SetActive(true);
    }

    public void HideAll() {
        for (int i = 0; i < allObjects.Count; i++)
        {
            allObjects[i].SetActive(false);
        }
    }

    public void DisableUI()
    {
        HideAll();
        canvasGameObject.SetActive(false);
        SceneSettingHelper.DisplayVR();
    }
    
    public void EnableUI()
    {
        SceneSettingHelper.Display2D();
        HideAll();
        canvasGameObject.SetActive(true);
    }

    #region Lobby

    public void Lobby_SetButtonVisabillity()
    {
        bool isCreator = OnlinePlayer.localPlayer.playerTarget == SessionTarget.create;
        readyButton.gameObject.SetActive(!isCreator);
        startButton.gameObject.SetActive(isCreator);
    }

    public void Lobby_CopyCode()
    {
        Debug.Log("TODO: Copy code");
    }

    public void Lobby_EmailCode()
    {
        Debug.Log("TODO: Email code");
    }

    public void Lobby_SetRole(int role)
    {
        OnlinePlayer.localPlayer.CmdSetRole((PlayerRole)role);
    }

    public void Lobby_ToogleReadyFlag()
    {
        bool currentState = OnlinePlayer.localPlayer.playerReady;
        OnlinePlayer.localPlayer.CmdSetReadyState(!currentState);
    }

    public void Lobby_LeaveLobby()
    {
        OnlinePlayer.localPlayer.LeaveLobby();
    }

    #endregion

    #region Actions

    public void Button_OpenMaengelliste()
    {
        Application.OpenURL("https://tobiasbohn.com/particle-rush/tobias_bohn_particle_rush_dokumentation_umsetzung.pdf");
    }

    public void Button_OpenTechnischeAnleitung()
    {
        Application.OpenURL("https://tobiasbohn.com/particle-rush/tobias_bohn_particle_rush_dokumentation_umsetzung.pdf");
    }

    public void Button_OpenCheckliste()
    {
        Application.OpenURL("https://tobiasbohn.com/particle-rush/tobias_bohn_particle_rush_dokumentation_umsetzung.pdf");
    }

    #endregion
}
