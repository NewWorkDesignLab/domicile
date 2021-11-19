using System.Collections.Generic;
using UnityEngine;
using Mirror;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/guides/networkbehaviour
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

// NOTE: Do not put objects in DontDestroyOnLoad (DDOL) in Awake.  You can do that in Start instead.

public class NetworkToogleStateExtension : NetworkBehaviour
{
    public ToggleStateHelper baseHelper;

    [SyncVar (hook = nameof (SetToggleState))]
    public bool currentState;

    public void SetToggleState(bool _, bool value)
    {
        baseHelper.SetToggleState(value);
    }

    [Command(requiresAuthority = false)]
    public void CmdToggle()
    {
        currentState = !currentState;
    }
}
