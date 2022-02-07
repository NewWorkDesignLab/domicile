using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;

public class LobbyUIManager : Singleton<LobbyUIManager>
{
    [Header("Lobby Prefabs")]
    public GameObject lobbyPlayerUIParent;
    public GameObject canvasGameObject;

    [Header("Lobby UI")]
    public GameObject lobbyGroup;
    public Button readyButton;
    public ButtonManagerBasic readyButtonMUIP;
    public Button startButton;

    [Header("Screenshot Gallerie")]
    public GameObject galerieParent;
    public GameObject galeriePrefab;
    public GameObject fullscreenParent;
    public GameObject fullscreenPrefab;

    [Header("Mängelliste")]
    public GameObject maengellistePrefab;
    public GameObject maengellisteParent;

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
        SessionInstance.instance.session.localRole = (PlayerRole)role;
    }

    public void Lobby_ToogleReadyFlag()
    {
        bool currentState = OnlinePlayer.localPlayer.playerReady;
        OnlinePlayer.localPlayer.CmdSetReadyState(!currentState);
        readyButtonMUIP.buttonText = currentState ? "Bereit" : "Nicht bereit";
        readyButtonMUIP.UpdateUI();
    }

    public void Lobby_LeaveLobby()
    {
        OnlinePlayer.localPlayer.LeaveLobby();
    }

    #endregion

    #region Actions

    public void Button_OpenTechnischeAnleitung()
    {
        Application.OpenURL("https://tobiasbohn.com/domcl/domicile_technische_hinweise_20220208.pdf");
    }

    public void Button_OpenCheckliste()
    {
        Application.OpenURL("https://tobiasbohn.com/domcl/domicile_checkliste_20220208.pdf");
    }

    #endregion

    public void InitGasllerieSetup()
    {
        StartCoroutine(SetupScreenshotGallerie());
    }

    private IEnumerator SetupScreenshotGallerie()
    {
        for (int i = 0; i < ScreenshotManager.takenScreenshots.Count; i++)
        {
            WWW www = new WWW ($"file:///{ScreenshotManager.takenScreenshots[i]}");
            while(!www.isDone)
                yield return null;

            GameObject fullscreen = Instantiate(fullscreenPrefab, fullscreenParent.transform);
            fullscreen.GetComponent<RawImage>().texture = www.texture;

            GameObject image = Instantiate(galeriePrefab, galerieParent.transform);
            image.GetComponent<RawImage>().texture = www.texture;
            image.GetComponent<Button>().onClick.AddListener(() => {
                fullscreen.SetActive(true);
            });
        }
    }
}
