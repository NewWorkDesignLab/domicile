using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using Mirror;

public class GeneralNetworkHelper : MonoBehaviour
{
    public GeneralNetworkHelperCondition condition;
    public GeneralNetworkHelperComparision comparision;

    public OnlineState valueOnlineState;
    public RoomCount valueRoomCount;
    public TextureDifficulty valueTextureDifficulty;
    public PlayerRole valuePlayerRole;

    public UnityEvent evntSuccess;

    void Start()
    {
        switch (condition)
        {
            case GeneralNetworkHelperCondition.onlineState:
                bool currentOnlineStatus = NetworkServer.active || NetworkClient.active;
                bool targetOnlineStatus = valueOnlineState == OnlineState.online;
                if ((currentOnlineStatus == targetOnlineStatus && comparision == GeneralNetworkHelperComparision.equals) ||
                    (currentOnlineStatus != targetOnlineStatus && comparision == GeneralNetworkHelperComparision.notEquals))
                    evntSuccess?.Invoke();
                break;

            case GeneralNetworkHelperCondition.scenarioRoomCount:
                if (NetworkServer.active || NetworkClient.active)
                    StartCoroutine(CheckRooms());
                break;

            case GeneralNetworkHelperCondition.scenarioTextureDifficulty:
                if (NetworkServer.active || NetworkClient.active)
                    StartCoroutine(CheckTexture());
                break;

            case GeneralNetworkHelperCondition.playerRole:
                if (NetworkServer.active || NetworkClient.active)
                    StartCoroutine(CheckPlayerRole(true));
                break;
        }
    }

    public void TestConsole(string text) {
        Debug.LogError(text);
    }

    private IEnumerator CheckRooms()
    {
        while (OnlinePlayer.scenario == null)
            yield return null;

        if ((OnlinePlayer.scenario.rooms == valueRoomCount && comparision == GeneralNetworkHelperComparision.equals) ||
            (OnlinePlayer.scenario.rooms != valueRoomCount && comparision == GeneralNetworkHelperComparision.notEquals))
            evntSuccess?.Invoke();
    }

    private IEnumerator CheckTexture()
    {
        while (OnlinePlayer.scenario == null)
            yield return null;

        if ((OnlinePlayer.scenario.textures == valueTextureDifficulty && comparision == GeneralNetworkHelperComparision.equals) ||
            (OnlinePlayer.scenario.textures != valueTextureDifficulty && comparision == GeneralNetworkHelperComparision.notEquals))
            evntSuccess?.Invoke();
    }

    private IEnumerator CheckPlayerRole(bool subscribeEvent = false)
    {
        while (OnlinePlayer.localPlayer == null)
            yield return null;

        if (subscribeEvent)
            OnlinePlayer.localPlayer.OnRoleChanged += OnRoleChanged;

        if ((OnlinePlayer.localPlayer.playerRole == valuePlayerRole && comparision == GeneralNetworkHelperComparision.equals) ||
            (OnlinePlayer.localPlayer.playerRole != valuePlayerRole && comparision == GeneralNetworkHelperComparision.notEquals))
            evntSuccess?.Invoke();
    }

    void OnRoleChanged(PlayerRole newRole)
    {
        StartCoroutine(CheckPlayerRole());
    }
}


public enum GeneralNetworkHelperCondition {
    onlineState, // if is online or offline
    scenarioRoomCount, // depending on room setting
    scenarioTextureDifficulty, // depending on texture setting
    playerRole // depending on online player role
}


public enum GeneralNetworkHelperComparision {
    equals,
    notEquals
}


[CustomEditor(typeof(GeneralNetworkHelper)), CanEditMultipleObjects]
public class GeneralNetworkHelperEditor : Editor
{
    public SerializedProperty condition_prop;
    public SerializedProperty comparision_prop;

    public SerializedProperty valueOnlineState_prop;
    public SerializedProperty valueRoomCount_prop;
    public SerializedProperty valueTextureDifficulty_prop;
    public SerializedProperty valuePlayerRole_prop;

    public SerializedProperty evntSuccess_prop;

    void OnEnable()
    {
        // Setup the SerializedProperties
        condition_prop = serializedObject.FindProperty ("condition");
        comparision_prop = serializedObject.FindProperty ("comparision");
        valueOnlineState_prop = serializedObject.FindProperty ("valueOnlineState");
        valueRoomCount_prop = serializedObject.FindProperty ("valueRoomCount");
        valueTextureDifficulty_prop = serializedObject.FindProperty ("valueTextureDifficulty");
        valuePlayerRole_prop = serializedObject.FindProperty ("valuePlayerRole");
        evntSuccess_prop = serializedObject.FindProperty ("evntSuccess");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update ();

        EditorGUILayout.PropertyField(condition_prop);
        EditorGUILayout.PropertyField(comparision_prop);
        GeneralNetworkHelperCondition status = (GeneralNetworkHelperCondition)condition_prop.enumValueIndex;
        
        switch(status)
        {
            case GeneralNetworkHelperCondition.onlineState:
                EditorGUILayout.PropertyField(valueOnlineState_prop, new GUIContent("Value"));
                break;

            case GeneralNetworkHelperCondition.scenarioRoomCount:
                EditorGUILayout.PropertyField(valueRoomCount_prop, new GUIContent("Value"));
                break;

            case GeneralNetworkHelperCondition.scenarioTextureDifficulty:
                EditorGUILayout.PropertyField(valueTextureDifficulty_prop, new GUIContent("Value"));
                break;

            case GeneralNetworkHelperCondition.playerRole:
                EditorGUILayout.PropertyField(valuePlayerRole_prop, new GUIContent("Value"));
                break;
        }
        
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(evntSuccess_prop, new GUIContent("Event if Condition True"));
        serializedObject.ApplyModifiedProperties ();
    }
}