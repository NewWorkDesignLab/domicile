using UnityEngine;
using Mirror;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-room-player
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkRoomPlayer.html
*/

/// <summary>
/// This component works in conjunction with the NetworkRoomManager to make up the multiplayer room system.
/// The RoomPrefab object of the NetworkRoomManager must have this component on it.
/// This component holds basic room player data required for the room to function.
/// Game specific data for room players can be put in other components on the RoomPrefab or in scripts derived from NetworkRoomPlayer.
/// </summary>
public class DomicileNetworkRoomPlayer : NetworkRoomPlayer
{
    public static DomicileNetworkRoomPlayer localPlayer;

    GameObject lobbyPlayerUI;

    public event System.Action<string> OnNameChanged;
    public event System.Action<bool> OnReadyStateChanged;
    public event System.Action<PlayerRole> OnRoleChanged;

    #region Player Vars

    [SyncVar(hook = nameof(NameChanged))]
    public string playerName;

    [SyncVar(hook = nameof(RoleChanged))]
    public int _role;
    public PlayerRole role
    {
        get { return (PlayerRole)_role; }
        set { _role = (int)value; }
    }

    [SyncVar(hook = nameof(GenderChanged))]
    public int _gender;
    public Gender gender
    {
        get { return (Gender)_gender; }
        set { _gender = (int)value; }
    }

    #endregion

    #region SyncVar Hooks

    private void NameChanged(string _, string newName)
    {
        OnNameChanged?.Invoke(newName);
    }

    private void RoleChanged(int _, int newValue)
    {
        OnRoleChanged?.Invoke((PlayerRole)newValue);
    }

    /// <summary>
    /// This is a hook that is invoked on clients when a RoomPlayer switches between ready or not ready.
    /// <para>This function is called when the a client player calls SendReadyToBeginMessage() or SendNotReadyToBeginMessage().</para>
    /// </summary>
    /// <param name="oldReadyState">The old readyState value</param>
    /// <param name="newReadyState">The new readyState value</param>
    public override void ReadyStateChanged(bool oldReadyState, bool newReadyState)
    {
        OnReadyStateChanged?.Invoke(newReadyState);
    }
    
    private void GenderChanged(int _, int newValue) {}

    #endregion


    #region Start & Stop Callbacks

    /// <summary>
    /// This is invoked for NetworkBehaviour objects when they become active on the server.
    /// <para>This could be triggered by NetworkServer.Listen() for objects in the scene, or by NetworkServer.Spawn() for objects that are dynamically created.</para>
    /// <para>This will be called for objects on a "host" as well as for object on a dedicated server.</para>
    /// </summary>
    public override void OnStartServer() { }

    /// <summary>
    /// Invoked on the server when the object is unspawned
    /// <para>Useful for saving object data in persistent storage</para>
    /// </summary>
    public override void OnStopServer() { }

    /// <summary>
    /// Called on every NetworkBehaviour when it is activated on a client.
    /// <para>Objects on the host have this function called, as there is a local client on the host. The values of SyncVars on object are guaranteed to be initialized correctly with the latest state from the server when this function is called on the client.</para>
    /// </summary>
    public override void OnStartClient()
    {
        lobbyPlayerUI = Instantiate(LobbyUIManager.instance.lobbyPlayerUIPrefab, LobbyUIManager.instance.lobbyPlayerUIParent.transform);

        // Set this player object in PlayerUI to wire up event handlers
        lobbyPlayerUI.GetComponent<LobbyPlayerUI>().SetPlayer(this, NetworkServer.active, isLocalPlayer);

        // Invoke all event handlers with the current data
        OnNameChanged?.Invoke(playerName);
        OnRoleChanged?.Invoke(role);
        OnReadyStateChanged?.Invoke(readyToBegin);
    }

    /// <summary>
    /// This is invoked on clients when the server has caused this object to be destroyed.
    /// <para>This can be used as a hook to invoke effects or do client specific cleanup.</para>
    /// </summary>
    public override void OnStopClient()
    {
        // Remove this player's UI object
        Destroy(lobbyPlayerUI);
    }

    /// <summary>
    /// Called when the local player object has been set up.
    /// <para>This happens after OnStartClient(), as it is triggered by an ownership message from the server. This is an appropriate place to activate components or functionality that should only be active for the local player, such as cameras and input.</para>
    /// </summary>
    public override void OnStartLocalPlayer()
    {
        localPlayer = this;
        CmdInitPlayer(SessionManager.session.name, SessionManager.session.gender, PlayerRole.spectator);
        if (SessionManager.session.target == SessionTarget.create)
        {
            CmdChangeReadyState(true);
            CmdGetAuthority();
        }
        CmdInitNetworkedScenario();
        // LobbyUIManager.instance.ShowUI();
    }

    [Command]
    private void CmdInitPlayer(string _name, Gender _gender, PlayerRole _role)
    {
        playerName = _name;
        gender = _gender;
        role = _role;
    }

    [Command]
    public void CmdSetRole(PlayerRole _role)
    {
        role = _role;
    }

    [Command]
    public void CmdGetAuthority()
    {
        SyncSceneProgress.instance.netIdentity.AssignClientAuthority(connectionToClient);
        NetworkedScenario.instance.netIdentity.AssignClientAuthority(connectionToClient);
    }

    [Command]
    public void CmdInitNetworkedScenario()
    {
        NetworkedScenario.instance.InitScenario(
            SessionManager.session.scenario,
            SessionManager.session.scenarioName,
            SessionManager.session.rooms,
            SessionManager.session.textures,
            SessionManager.session.report,
            SessionManager.session.tenant,
            SessionManager.session.contract,
            SessionManager.session.protocol
        );
    }

    [Command]
    public void CmdSetSceneIngame()
    {
        DomicileNetworkRoomManager.instance.ServerChangeScene(DomicileNetworkRoomManager.instance.GameplayScene);
    }

    [Command]
    public void CmdSetSceneLobby()
    {
        DomicileNetworkRoomManager.instance.ServerChangeScene(DomicileNetworkRoomManager.instance.RoomScene);
    }

    public void LeaveLobby()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            DomicileNetworkRoomManager.instance.StopHost();
        }
        else if (NetworkClient.isConnected)
        {
            DomicileNetworkRoomManager.instance.StopClient();
        }
        else if (NetworkServer.active)
        {
            DomicileNetworkRoomManager.instance.StopServer();
        }
    }

    /// <summary>
    /// This is invoked on behaviours that have authority, based on context and <see cref="NetworkIdentity.hasAuthority">NetworkIdentity.hasAuthority</see>.
    /// <para>This is called after <see cref="OnStartServer">OnStartServer</see> and before <see cref="OnStartClient">OnStartClient.</see></para>
    /// <para>When <see cref="NetworkIdentity.AssignClientAuthority"/> is called on the server, this will be called on the client that owns the object. When an object is spawned with <see cref="NetworkServer.Spawn">NetworkServer.Spawn</see> with a NetworkConnection parameter included, this will be called on the client that owns the object.</para>
    /// </summary>
    public override void OnStartAuthority() { }

    /// <summary>
    /// This is invoked on behaviours when authority is removed.
    /// <para>When NetworkIdentity.RemoveClientAuthority is called on the server, this will be called on the client that owns the object.</para>
    /// </summary>
    public override void OnStopAuthority() { }

    #endregion

    #region Room Client Callbacks

    /// <summary>
    /// This is a hook that is invoked on all player objects when entering the room.
    /// <para>Note: isLocalPlayer is not guaranteed to be set until OnStartLocalPlayer is called.</para>
    /// </summary>
    public override void OnClientEnterRoom() { }

    /// <summary>
    /// This is a hook that is invoked on all player objects when exiting the room.
    /// </summary>
    public override void OnClientExitRoom() { }

    #endregion

    #region SyncVar Hooks

    /// <summary>
    /// This is a hook that is invoked on clients when the index changes.
    /// </summary>
    /// <param name="oldIndex">The old index value</param>
    /// <param name="newIndex">The new index value</param>
    public override void IndexChanged(int oldIndex, int newIndex) { }

    

    #endregion

    #region Optional UI

    public override void OnGUI()
    {
        base.OnGUI();
    }

    #endregion
}
