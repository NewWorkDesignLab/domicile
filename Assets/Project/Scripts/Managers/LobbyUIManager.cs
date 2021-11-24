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

    [Header("Screenshot Gallerie")]
    public GameObject galerieParent;
    public GameObject screenshotPrefab;

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
    }

    public void Lobby_LeaveLobby()
    {
        OnlinePlayer.localPlayer.LeaveLobby();
    }

    #endregion

    #region Actions

    public void Button_OpenMaengelliste()
    {
        if (OnlinePlayer.scenario?.rooms == RoomCount.two)
            Application.OpenURL("https://tobiasbohn.com/domcl/OD-5_M%C3%A4ngelliste_2-Zimmer-Whg_Atzenbeck.pdf");
        else if (OnlinePlayer.scenario?.rooms == RoomCount.three)
            Application.OpenURL("https://tobiasbohn.com/domcl/OD-5_M%C3%A4ngelliste_3-Zimmer-Whg_Gebhard.pdf");
    }

    public void Button_OpenTechnischeAnleitung()
    {
        Application.OpenURL("https://tobiasbohn.com/particle-rush/tobias_bohn_particle_rush_dokumentation_umsetzung.pdf");
    }

    public void Button_OpenCheckliste()
    {
        Application.OpenURL("https://tobiasbohn.com/domcl/OD-6_Checkliste.pdf");
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

            GameObject image = Instantiate(screenshotPrefab, galerieParent.transform);
            image.GetComponent<RawImage>().texture = www.texture;
        }
    }
}
