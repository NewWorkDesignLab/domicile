using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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