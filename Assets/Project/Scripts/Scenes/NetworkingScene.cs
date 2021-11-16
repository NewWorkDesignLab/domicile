using UnityEngine;
using Mirror;

public class NetworkingScene : MonoBehaviour
{
    void Start()
    {
#if UNITY_STANDALONE_LINUX
        DomicileNetManager.instance.StartServer();
#else
        DomicileNetManager.instance.StartClient();
#endif
    }
}
