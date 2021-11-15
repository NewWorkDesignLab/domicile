using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public GameObject cameraPrefab;

    public void InitCamera()
    {
        Instantiate(cameraPrefab, transform);
    }
}
