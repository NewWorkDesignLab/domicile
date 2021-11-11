using UnityEngine;
using Mirror;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-room-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkRoomManager.html

	See Also: NetworkManager
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

/// <summary>
/// This is a specialized NetworkManager that includes a networked room.
/// The room has slots that track the joined players, and a maximum player count that is enforced.
/// It requires that the NetworkRoomPlayer component be on the room player objects.
/// NetworkRoomManager is derived from NetworkManager, and so it implements many of the virtual functions provided by the NetworkManager class.
/// </summary>
public class DomicileNetworkRoomManager : NetworkRoomManager
{
    public static DomicileNetworkRoomManager instance;

    private bool enableStartButton = false;

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    [ServerCallback]
    void Update()
    {
        if (!allPlayersReady && enableStartButton)
        {
            enableStartButton = false;
            OnRoomServerPlayersNotReady();
        }
    }

    /// <summary>
    /// This is called on the server when all the players in the room are ready.
    /// <para>The default implementation of this function uses ServerChangeScene() to switch to the game player scene. By implementing this callback you can customize what happens when all the players in the room are ready, such as adding a countdown or a confirmation for a group leader.</para>
    /// </summary>
    [Server]
    public override void OnRoomServerPlayersReady()
    {
        // base.OnRoomServerPlayersReady();
        enableStartButton = true;
        Debug.Log("ALL PLAYERS READY");
        LobbyUIManager.instance.Lobby_EnableStartButton(true);
    }

    /// <summary>
    /// This is called on the server when all the players previouly were ready but someone cancels ready-state.
    /// </summary>
    [Server]
    public override void OnRoomServerPlayersNotReady()
    {
        Debug.Log("ALL PLAYERS READY CANCELD");
        LobbyUIManager.instance.Lobby_EnableStartButton(false);
    }
}
