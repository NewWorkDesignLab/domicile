using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public bool enableCamServer = false;
    public bool enableCamNotServer = false;
    public Camera camBehaviour;

    void Start()
    {
#if UNITY_STANDALONE_LINUX
        if (enableCamServer) InitCamera();
#else
        if (enableCamNotServer) InitCamera();
#endif
    }

    public void InitCamera()
    {
        camBehaviour.enabled = true;
    }
}
