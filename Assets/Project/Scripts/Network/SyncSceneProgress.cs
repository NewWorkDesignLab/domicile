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
    private int sceneStatus = 0;

    private void SceneStatusChanged(int _, int newStatus)
    {
        ApplySceneStatus();
    }

    public void ApplySceneStatus()
    {
        if (sceneStatus < SceneSyncSettings.Length && OnlinePlayer.localPlayer != null)
        {
            SceneSyncSettings[sceneStatus].OnSceneSyncAll?.Invoke();
            if (OnlinePlayer.localPlayer.playerRole == PlayerRole.guide) SceneSyncSettings[sceneStatus].OnSceneSyncGuide?.Invoke();
            if (OnlinePlayer.localPlayer.playerRole == PlayerRole.learner) SceneSyncSettings[sceneStatus].OnSceneSyncLearner?.Invoke();
            if (OnlinePlayer.localPlayer.playerRole == PlayerRole.spectator) SceneSyncSettings[sceneStatus].OnSceneSyncSpectator?.Invoke();
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdNextSceneStatus()
    {
        if (sceneStatus < SceneSyncSettings.Length - 1) sceneStatus++;
    }

    [Command(requiresAuthority = false)]
    public void CmdPreviousSceneStatus()
    {
        if (sceneStatus > 0) sceneStatus--;
    }

    [Command(requiresAuthority = false)]
    public void CmdSetSceneStatusDirectly(int index)
    {
        sceneStatus = index;
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
