using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyUIManager : Singleton<LobbyUIManager>
{
    [Header("Lobby Prefabs")]
    public GameObject lobbyPlayerUIPrefab;
    public GameObject lobbyPlayerUIParent;

    [Header("Lobby UI")]
    public GameObject lobbyGroup;
    public TMP_Text lobbyCode;
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


    public void Start()
    {
        HideAll();
        lobbyGroup.SetActive(true);
        Lobby_UpdateUI();
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
    }

    #region Lobby

    public void Lobby_UpdateUI()
    {
        // Join / Ready Button
        bool isCreator = SessionManager.session.target == SessionTarget.create;
        readyButton.gameObject.SetActive(!isCreator);
        startButton.gameObject.SetActive(isCreator);
    }

    public void Lobby_EnableStartButton(bool value)
    {
        startButton.interactable = value;
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
        Debug.Log("Set Role");
        // TestRoomPlayer.localRoomPlayer.CmdSetTest(Random.Range(0, 100));
    }

    public void Lobby_ToogleReadyFlag()
    {
        // bool currentState = DomicileNetworkRoomPlayer.localRoomPlayer.readyToBegin;
        // DomicileNetworkRoomPlayer.localRoomPlayer.CmdChangeReadyState(!currentState);
    }

    public void Lobby_LeaveLobby()
    {
        // DomicileNetworkRoomPlayer.localRoomPlayer.LeaveLobby();
    }

    #endregion
}
