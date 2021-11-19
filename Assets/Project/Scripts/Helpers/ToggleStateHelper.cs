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
    private bool currentState;

    public NetworkToogleStateExtension netExtension;

    void Start ()
    {
        // close on start
        SetToggleState(false);
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
