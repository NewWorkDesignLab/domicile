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
#if UNITY_ANDROID
        if (settingOnSceneStart == VRSetting.VR)
        {
            DisplayVR();
        }
        else
        {
            SwitchTo2D ();
        }
#endif
    }

    public static void DisplayVR()
    {
        if (current == "vr")
        {
            Debug.Log("Display Setting already VR. Skipping setup.");
            return;
        }
        current = "vr";
#if UNITY_ANDROID
        instance.StartCoroutine (SwitchToVR ());
#endif
    }

    private static IEnumerator SwitchToVR ()
    {
        // from https://forum.unity.com/threads/toggle-between-2d-and-google-cardboard.902378/
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        yield return new WaitForSeconds (.5f);
#if UNITY_EDITOR
        Debug.Log ("[SceneSettingHelper SwitchToVR] Would Display Scene in Virtual Reality");
        yield return null;
#else
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader ();
        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError ("[SceneSettingHelper SwitchToVR] Initializing XR Failed. Check Editor or Player log for details.");
        }
        else
        {
            Debug.Log ("[SceneSettingHelper SwitchToVR] Starting XR...");
            XRGeneralSettings.Instance.Manager.StartSubsystems ();
        }
#endif
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.brightness = 1.0f;
    }

    public static void SwitchTo2D ()
    {
        if (current == "normal")
        {
            Debug.Log("Display Setting already Normal. Skipping setup.");
            return;
        }
        current = "normal";

#if UNITY_EDITOR
        Debug.Log ("[SceneSettingHelper SwitchTo2D] Would Display Scene in Normal Mode");
#else
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems ();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader ();
        }
#endif
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }
}

public enum VRSetting { Normal, VR }