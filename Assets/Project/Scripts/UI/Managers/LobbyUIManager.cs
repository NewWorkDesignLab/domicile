using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Michsky.UI.ModernUIPack;

public class LobbyUIManager : Singleton<LobbyUIManager>
{
    [Header("Lobby Prefabs")]
    public GameObject lobbyPlayerUIPrefab;
    public GameObject lobbyPlayerUIParent;

    [Header("Lobby UI")]
    public TMP_Text scenarioName;
    public ButtonManagerBasic lobbyDisplayReport;
    public TMP_Text lobbyCode;

    void Start()
    {
        Lobby_UpdateUI();
    }

    public void Lobby_UpdateUI()
    {
        // Scenario Name Display
        scenarioName.text = SessionManager.session.scenarioName;

        // Case Report Button
        string reportTxt = TextGenerator.GenerateReportText(SessionManager.session.tenant, SessionManager.session.contract, SessionManager.session.protocol);
        lobbyDisplayReport.buttonText = reportTxt;
        lobbyDisplayReport.UpdateUI();
    }

    public void Lobby_OpenReport()
    {
        Application.OpenURL("https://tobiasbohn.com/particle-rush/tobias_bohn_particle_rush_dokumentation_umsetzung.pdf");
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
        DomicileNetworkRoomPlayer.localPlayer.CmdSetRole((PlayerRole)role);
    }

    public void Lobby_ToogleReadyFlag()
    {
        bool currentState = DomicileNetworkRoomPlayer.localPlayer.readyToBegin;
        DomicileNetworkRoomPlayer.localPlayer.CmdChangeReadyState(!currentState);
    }

    public void Lobby_LeaveLobby()
    {
        DomicileNetworkRoomPlayer.localPlayer.LeaveLobby();
    }
}
