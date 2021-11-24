using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ToggleStateHelper : MonoBehaviour
{
    public GameObject[] activeOnTrue;
    public GameObject[] activeOnFalse;
    
    // [SyncVar (hook = nameof (SetCurrentState))]
    // true = open; false = close
    public bool currentState = false;

    public NetworkToogleStateExtension netExtension;

    void Start ()
    {
        // user currentState as default Value on Start
        SetToggleState(currentState);
    }

    public void SetToggleState(bool value)
    {
        currentState = value;
        if (activeOnTrue != null) {
            for (int i = 0; i < activeOnTrue.Length; i++) {
                activeOnTrue[i].SetActive(currentState);
            }
        }
        if (activeOnFalse != null) {
            for (int i = 0; i < activeOnFalse.Length; i++) {
                activeOnFalse[i].SetActive(!currentState);
            }
        }
    }

    public void ToogleState()
    {
        bool isNetworked = NetworkServer.active || NetworkClient.active;
        if (!isNetworked) SetToggleState(!currentState);
        if (isNetworked) netExtension.CmdToggle();
    }
}
