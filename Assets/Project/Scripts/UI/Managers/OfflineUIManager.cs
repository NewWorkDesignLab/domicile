using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;
using Michsky.UI.ModernUIPack;

public class OfflineUIManager : Singleton<OfflineUIManager>
{
    [Header("Main Landing UI")]
    public GameObject mainLandingGroup;
    public Button mainLandingButton;

    [Header("Main Menu UI")]
    public GameObject mainMenuGroup;
    public TMP_Text mainMenuText;

    [Header("Create Scenario A UI")]
    public GameObject scenarioCreateGroupA;
    
    [Header("Create Scenario B UI")]
    public GameObject scenarioCreateGroupB;
    public ButtonManagerBasic scenarioCreateDisplayRooms;
    public ButtonManagerBasic scenarioCreateDisplayTextures;
    public ButtonManagerBasic scenarioCreateDisplayReport;
    
    [Header("Join Scenario UI")]
    public GameObject scJoinGroup;

    protected override void Awake()
    {
        base.Awake();
        HideAll();
        mainLandingGroup.SetActive(true);
    }

    public void HideAll()
    {
        mainLandingGroup.SetActive(false);
        mainMenuGroup.SetActive(false);
        scenarioCreateGroupA.SetActive(false);
        scenarioCreateGroupB.SetActive(false);
        scJoinGroup.SetActive(false);
    }

    #region Main Landing UI

    public void MainLanding_CheckButtonInteraction()
    {
        bool nameReady = SessionManager.session.IsValidName();
        bool genderReady = SessionManager.session.gender != Gender.unspecified;
        bool consentReady = SessionManager.session.consent;
        mainLandingButton.interactable = nameReady && genderReady && consentReady;
    }

    #endregion

    #region Main Menu

    public void MainLanding_SetGreeting()
    {
        mainMenuText.text = $"Hallo {SessionManager.session.name}. Was möchten Sie tun?";
    }

    #endregion
    
    #region Create Scenario

    public void CreateScenario_UpdateRoomDisplay()
    {
        string txt = TextGenerator.GenerateRoomText(SessionManager.session.rooms);
        scenarioCreateDisplayRooms.buttonText = txt;
        scenarioCreateDisplayRooms.UpdateUI();
    }

    public void CreateScenario_UpdateTextureDisplay()
    {
        string txt = TextGenerator.GenerateTextureText(SessionManager.session.textures);
        scenarioCreateDisplayTextures.buttonText = txt;
        scenarioCreateDisplayTextures.UpdateUI();
    }

    public void CreateScenario_UpdateReportDisplay()
    {
        string txt = TextGenerator.GenerateReportText(SessionManager.session.tenant, SessionManager.session.contract, SessionManager.session.protocol);
        scenarioCreateDisplayReport.buttonText = txt;
        scenarioCreateDisplayReport.UpdateUI();
    }

    public void CreateScenario_CreateLobby()
    {
        DomicileNetworkRoomManager.instance.StartClient();
    }

    public void CreateScenario_JoinLobby()
    {
        DomicileNetworkRoomManager.instance.StartClient();
    }

    #endregion
}
