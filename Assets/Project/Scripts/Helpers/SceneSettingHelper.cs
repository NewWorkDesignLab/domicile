using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Management;

public class SceneSettingHelper : Singleton<SceneSettingHelper>
{
    public VRSetting settingOnSceneStart;
    private static string current = "";

    void Start ()
    {
        if (settingOnSceneStart == VRSetting.VR)
            DisplayVR ();
        else
            Display2D ();
    }

    public static void Display2D ()
    {
        // skip if already set
        if (current == "normal") return;
        current = "normal";

        #if UNITY_ANDROID && !UNITY_EDITOR
        // execute only if XR Manager is available and init complete
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems ();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader ();
        }

        // set screen settings
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
        #endif
    }

    public static void DisplayVR()
    {
        // skip if already set
        if (current == "vr") return;
        current = "vr";

        #if UNITY_ANDROID && !UNITY_EDITOR
        instance.StartCoroutine (SwitchToVR ());
        #endif
    }

    private static IEnumerator SwitchToVR ()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        // from https://forum.unity.com/threads/toggle-between-2d-and-google-cardboard.902378/
        // set screen setting orientation
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        yield return new WaitForSeconds (.5f);

        // init xr loader and wait for completion
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader ();

        // start subsystems
        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
            Debug.LogError ("[SceneSettingHelper SwitchToVR] Initializing XR Failed. Check Editor or Player log for details.");
        else
            XRGeneralSettings.Instance.Manager.StartSubsystems ();
        
        // set screen settings
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.brightness = 1.0f;
        #else
        yield break;
        #endif
    }
}

public enum VRSetting { Normal, VR }