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
    public SliderManager scenarioCreateADiffSlider;
    
    [Header("Create Scenario B UI")]
    public GameObject scenarioCreateGroupB;
    public ButtonManagerBasic scenarioCreateDisplayRooms;
    public ButtonManagerBasic scenarioCreateDisplayTextures;
    public ButtonManagerBasic scenarioCreateDisplayReport;
    public SliderManager scenarioCreateBDiffSlider;
    
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

    public void CreateScenario_UpdateDifficultyIndicator()
    {
        float selectedDiff = SessionInstance.instance.session.difficulty;
        TextureDifficulty selectedTexture = SessionInstance.instance.session.textures;

        float calculatedDiff = selectedTexture switch
        {
            TextureDifficulty.easy => selectedDiff.Remap(1, 5, 8, 12),
            TextureDifficulty.medium => selectedDiff.Remap(1, 5, 12, 16.5f),
            TextureDifficulty.hard => selectedDiff.Remap(1, 5, 12, 16.5f),
            _ => selectedDiff.Remap(1, 5, 8, 16.5f)
        };

        if (SessionInstance.instance.session.rooms == RoomCount.three)
        {
            calculatedDiff *= 1.16f;
        }

        // float SLIDER_MIN = 8.0f;
        // float SLIDER_MAX = 19.14f;

        scenarioCreateADiffSlider.mainSlider.value = calculatedDiff;
        scenarioCreateBDiffSlider.mainSlider.value = calculatedDiff;
        Debug.Log(calculatedDiff);
    }

    #endregion
}
