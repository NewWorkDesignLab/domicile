using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DisableOnlineHelper : MonoBehaviour
{
    public bool disableOnline;

    // Start is called before the first frame update
    void Start()
    {
        bool onlineStatus = NetworkServer.active || NetworkClient.active;
        bool match = onlineStatus && disableOnline;
        gameObject.SetActive(!match);
    }
}
