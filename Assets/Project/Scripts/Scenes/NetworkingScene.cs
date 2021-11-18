using UnityEngine;
using Mirror;

public class NetworkingScene : Singleton<NetworkingScene>
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
