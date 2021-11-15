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

    #region SyncVar

    [SyncVar(hook = nameof(SceneStatusChanged))]
    private int sceneStatus = -1;

    private void SceneStatusChanged(int _, int newStatus)
    {
        if (newStatus < SceneSyncSettings.Length)
        {
            SceneSyncSettings[newStatus].OnSceneSyncAll?.Invoke();
            // if (DomicileNetworkRoomPlayer.localRoomPlayer.role == PlayerRole.guide) SceneSyncSettings[newStatus].OnSceneSyncGuide?.Invoke();
            // if (DomicileNetworkRoomPlayer.localRoomPlayer.role == PlayerRole.learner) SceneSyncSettings[newStatus].OnSceneSyncLearner?.Invoke();
            // if (DomicileNetworkRoomPlayer.localRoomPlayer.role == PlayerRole.spectator) SceneSyncSettings[newStatus].OnSceneSyncSpectator?.Invoke();
        }
        else
        {
            // DomicileNetworkRoomPlayer.localRoomPlayer.CmdSetSceneIngame();
        }
    }

    [Command]
    public void CmdNextSceneStatus()
    {
        sceneStatus++;
    }

    #endregion

    public SceneSyncSetting[] SceneSyncSettings;
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
