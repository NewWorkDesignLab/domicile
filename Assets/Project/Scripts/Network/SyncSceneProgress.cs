using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;
using System;

public class SyncSceneProgress : NetworkBehaviour
{
    #region Singleton

    public static SyncSceneProgress instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public SceneSyncSetting[] SceneSyncSettings;

    [SyncVar(hook = nameof(SceneStatusChanged))]
    private int sceneStatus = -1;

    private void SceneStatusChanged(int _, int newStatus)
    {
        if (newStatus < SceneSyncSettings.Length)
        {
            SceneSyncSettings[newStatus].OnSceneSyncAll?.Invoke();
            if (OnlinePlayer.localPlayer.playerRole == PlayerRole.guide) SceneSyncSettings[newStatus].OnSceneSyncGuide?.Invoke();
            if (OnlinePlayer.localPlayer.playerRole == PlayerRole.learner) SceneSyncSettings[newStatus].OnSceneSyncLearner?.Invoke();
            if (OnlinePlayer.localPlayer.playerRole == PlayerRole.spectator) SceneSyncSettings[newStatus].OnSceneSyncSpectator?.Invoke();
        }
        else
        {
            // DomicileNetworkRoomPlayer.localRoomPlayer.CmdSetSceneIngame();
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdNextSceneStatus()
    {
        sceneStatus++;
    }
}


[Serializable]
public class SceneSyncEvent : UnityEvent {}

[Serializable]
public class SceneSyncSetting
{
    public SceneSyncEvent OnSceneSyncAll;
    public SceneSyncEvent OnSceneSyncGuide;
    public SceneSyncEvent OnSceneSyncLearner;
    public SceneSyncEvent OnSceneSyncSpectator;
}
