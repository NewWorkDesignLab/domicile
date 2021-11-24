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
    
    [Header("Loading UI")]
    public GameObject loadingGroup;
    
    [Header("Reconnect UI")]
    public GameObject reconnectGroup;

    public void HideAll()
    {
        mainLandingGroup.SetActive(false);
        mainMenuGroup.SetActive(false);
        scenarioCreateGroupA.SetActive(false);
        scenarioCreateGroupB.SetActive(false);
        scJoinGroup.SetActive(false);
        offlineGroup.SetActive(false);
        loadingGroup.SetActive(false);
        reconnectGroup.SetActive(false);
    }

    #region Main Landing UI

    public void MainLanding_CheckButtonInteraction()
    {
        bool nameReady = SessionInstance.instance.session.IsValidName();
        bool genderReady = SessionInstance.instance.session.gender != Gender.unspecified;
        bool consentReady = SessionInstance.instance.session.consent;
        mainLandingButton.interactable = nameReady && genderReady && consentReady;
    }

    #endregion

    #region Main Menu

    public void MainLanding_SetGreeting()
    {
        mainMenuText.text = $"Hallo {SessionInstance.instance.session.name}. Was möchten Sie tun?";
    }

    #endregion
    
    #region Create Scenario

    public void CreateScenario_UpdateRoomDisplay()
    {
        string txt = TextGenerator.GenerateRoomText(SessionInstance.instance.session.rooms);
        scenarioCreateDisplayRooms.buttonText = txt;
        scenarioCreateDisplayRooms.UpdateUI();
    }

    public void CreateScenario_UpdateTextureDisplay()
    {
        string txt = TextGenerator.GenerateTextureText(SessionInstance.instance.session.textures);
        scenarioCreateDisplayTextures.buttonText = txt;
        scenarioCreateDisplayTextures.UpdateUI();
    }

    public void CreateScenario_UpdateReportDisplay()
    {
        string txt = TextGenerator.GenerateReportText(SessionInstance.instance.session.tenant, SessionInstance.instance.session.contract, SessionInstance.instance.session.protocol);
        scenarioCreateDisplayReport.buttonText = txt;
        scenarioCreateDisplayReport.UpdateUI();
    }

    #endregion
}
