using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;
using Michsky.UI.ModernUIPack;
using UnityEngine.SceneManagement;

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

    [Header("Offline UI")]
    public GameObject offlineGroup;

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
        offlineGroup.SetActive(false);
    }

    #region Main Landing UI

    public void MainLanding_CheckButtonInteraction()
    {
        bool nameReady = SessionManager.instance.session.IsValidName();
        bool genderReady = SessionManager.instance.session.gender != Gender.unspecified;
        bool consentReady = SessionManager.instance.session.consent;
        mainLandingButton.interactable = nameReady && genderReady && consentReady;
    }

    #endregion

    #region Main Menu

    public void MainLanding_SetGreeting()
    {
        mainMenuText.text = $"Hallo {SessionManager.instance.session.name}. Was möchten Sie tun?";
    }

    #endregion
    
    #region Create Scenario

    public void CreateScenario_UpdateRoomDisplay()
    {
        string txt = TextGenerator.GenerateRoomText(SessionManager.instance.session.rooms);
        scenarioCreateDisplayRooms.buttonText = txt;
        scenarioCreateDisplayRooms.UpdateUI();
    }

    public void CreateScenario_UpdateTextureDisplay()
    {
        string txt = TextGenerator.GenerateTextureText(SessionManager.instance.session.textures);
        scenarioCreateDisplayTextures.buttonText = txt;
        scenarioCreateDisplayTextures.UpdateUI();
    }

    public void CreateScenario_UpdateReportDisplay()
    {
        string txt = TextGenerator.GenerateReportText(SessionManager.instance.session.tenant, SessionManager.instance.session.contract, SessionManager.instance.session.protocol);
        scenarioCreateDisplayReport.buttonText = txt;
        scenarioCreateDisplayReport.UpdateUI();
    }

    #endregion
}
