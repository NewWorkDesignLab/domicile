using UnityEngine;
using Mirror;

public class NetworkingScene : MonoBehaviour
{
    void Start()
    {
#if UNITY_STANDALONE_LINUX
        ((DomicileNetManager)NetworkManager.singleton).StartServer();
#else
        ((DomicileNetManager)NetworkManager.singleton).StartClient();
#endif
    }
}
