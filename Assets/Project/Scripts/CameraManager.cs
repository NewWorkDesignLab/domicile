using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public bool enableCamOnStart = false;
    public Camera camBehaviour;

    void Start()
    {
        if (enableCamOnStart) InitCamera();
    }

    public void InitCamera()
    {
        camBehaviour.enabled = true;
    }
}
