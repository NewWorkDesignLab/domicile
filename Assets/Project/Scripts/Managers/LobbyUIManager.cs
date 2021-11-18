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
    public GameObject stage_1_A_G;
    public GameObject stage_1_B_G;
    public GameObject stage_1_C_G;
    public GameObject stage_1_A_L;
    public GameObject stage_1_BC_L;
    public GameObject stage_2_A_G;
    public GameObject stage_2_B_G;
    public GameObject stage_2_C_G;
    public GameObject stage_2_D_G;
    public GameObject stage_2_E_G;
    public GameObject stage_2_F_G;
    public GameObject stage_2_AB_L;
    public GameObject stage_2_CD_L;
    public GameObject stage_2_EF_L;
    public GameObject stage_12_SPECTATOR;
    public GameObject loading;

    public GameObject tmpNachbereitung;


    public void Start()
    {
        HideAll();
        loading.SetActive(true);
    }

    public void HideAll() {
        lobbyGroup.SetActive(false);
        stage_1_A_G.SetActive(false);
        stage_1_B_G.SetActive(false);
        stage_1_C_G.SetActive(false);
        stage_1_A_L.SetActive(false);
        stage_1_BC_L.SetActive(false);
        stage_2_A_G.SetActive(false);
        stage_2_B_G.SetActive(false);
        stage_2_C_G.SetActive(false);
        stage_2_D_G.SetActive(false);
        stage_2_E_G.SetActive(false);
        stage_2_F_G.SetActive(false);
        stage_2_AB_L.SetActive(false);
        stage_2_CD_L.SetActive(false);
        stage_2_EF_L.SetActive(false);
        stage_12_SPECTATOR.SetActive(false);
        loading.SetActive(false);
        tmpNachbereitung.SetActive(false);
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
}
