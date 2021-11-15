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
    public static DomicileNetworkRoomPlayer localRoomPlayer;

    // GameObject lobbyPlayerUI;

    // public event System.Action<string> OnNameChanged;
    // public event System.Action<bool> OnReadyStateChanged;
    // public event System.Action<PlayerRole> OnRoleChanged;

    // #region Player Vars

    // [SyncVar(hook = nameof(NameChanged))]
    // public string playerName;

    // [SyncVar(hook = nameof(RoleChanged))]
    // public int _role;
    // public PlayerRole role
    // {
    //     get { return (PlayerRole)_role; }
    //     set { _role = (int)value; }
    // }

    // [SyncVar(hook = nameof(GenderChanged))]
    // public int _gender;
    // public Gender gender
    // {
    //     get { return (Gender)_gender; }
    //     set { _gender = (int)value; }
    // }

    // #endregion

    // #region SyncVar Hooks

    // private void NameChanged(string _, string newName)
    // {
    //     OnNameChanged?.Invoke(newName);
    // }

    // private void RoleChanged(int _, int newValue)
    // {
    //     OnRoleChanged?.Invoke((PlayerRole)newValue);
    // }

    // /// <summary>
    // /// This is a hook that is invoked on clients when a RoomPlayer switches between ready or not ready.
    // /// <para>This function is called when the a client player calls SendReadyToBeginMessage() or SendNotReadyToBeginMessage().</para>
    // /// </summary>
    // /// <param name="oldReadyState">The old readyState value</param>
    // /// <param name="newReadyState">The new readyState value</param>
    // public override void ReadyStateChanged(bool oldReadyState, bool newReadyState)
    // {
    //     OnReadyStateChanged?.Invoke(newReadyState);
    // }
    
    // private void GenderChanged(int _, int newValue) {}

    // #endregion


    // #region Start & Stop Callbacks

    // /// <summary>
    // /// Called on every NetworkBehaviour when it is activated on a client.
    // /// <para>Objects on the host have this function called, as there is a local client on the host. The values of SyncVars on object are guaranteed to be initialized correctly with the latest state from the server when this function is called on the client.</para>
    // /// </summary>
    // public override void OnStartClient()
    // {
    //     lobbyPlayerUI = Instantiate(LobbyUIManager.instance.lobbyPlayerUIPrefab, LobbyUIManager.instance.lobbyPlayerUIParent.transform);

    //     // Set this player object in PlayerUI to wire up event handlers
    //     // lobbyPlayerUI.GetComponent<LobbyPlayerUI>().SetPlayer(this);

    //     // Invoke all event handlers with the current data
    //     OnNameChanged?.Invoke(playerName);
    //     OnRoleChanged?.Invoke(role);
    //     OnReadyStateChanged?.Invoke(readyToBegin);
    // }

    // /// <summary>
    // /// This is invoked on clients when the server has caused this object to be destroyed.
    // /// <para>This can be used as a hook to invoke effects or do client specific cleanup.</para>
    // /// </summary>
    // public override void OnStopClient()
    // {
    //     // Remove this player's UI object
    //     Destroy(lobbyPlayerUI);
    // }

    /// <summary>
    /// Called when the local player object has been set up.
    /// <para>This happens after OnStartClient(), as it is triggered by an ownership message from the server. This is an appropriate place to activate components or functionality that should only be active for the local player, such as cameras and input.</para>
    /// </summary>
    public override void OnStartLocalPlayer()
    {
        localRoomPlayer = this;
    //     CmdInitPlayer(SessionManager.session.name, SessionManager.session.gender, PlayerRole.spectator);
    //     if (SessionManager.session.target == SessionTarget.create)
    //     {
    //         CmdChangeReadyState(true);
    //         CmdGetAuthority();
    //         CmdInitNetworkedScenario(
    //             SessionManager.session.scenario,
    //             SessionManager.session.scenarioName,
    //             SessionManager.session.rooms,
    //             SessionManager.session.textures,
    //             SessionManager.session.report,
    //             SessionManager.session.tenant,
    //             SessionManager.session.contract,
    //             SessionManager.session.protocol
    //         );
    //     }
    }

    // #endregion

    // [Command]
    // private void CmdInitPlayer(string _name, Gender _gender, PlayerRole _role)
    // {
    //     playerName = _name;
    //     gender = _gender;
    //     role = _role;
    // }

    // [Command]
    // public void CmdSetRole(PlayerRole _role)
    // {
    //     role = _role;
    // }

    // [Command]
    // public void CmdGetAuthority()
    // {
    //     SyncSceneProgress.instance.netIdentity.AssignClientAuthority(connectionToClient);
    //     NetworkedScenario.instance.netIdentity.AssignClientAuthority(connectionToClient);
    // }

    // [Command]
    // public void CmdInitNetworkedScenario(string _id, string _name, RoomCount _rooms, TextureDifficulty _textures, CaseReport _report, Tenant _tenant, RentalContract _contract, HandoverProtocol _protocol)
    // {
    //     NetworkedScenario.instance.InitScenario(_id, _name, _rooms, _textures, _report, _tenant, _contract, _protocol);
    // }

    // [Command]
    // public void CmdSetSceneIngame()
    // {
    //     DomicileNetworkRoomManager.instance.ServerChangeScene(DomicileNetworkRoomManager.instance.GameplayScene);
    // }

    // [Command]
    // public void CmdSetSceneLobby()
    // {
    //     DomicileNetworkRoomManager.instance.ServerChangeScene(DomicileNetworkRoomManager.instance.RoomScene);
    // }

    // public void LeaveLobby()
    // {
    //     if (NetworkServer.active && NetworkClient.isConnected)
    //     {
    //         DomicileNetworkRoomManager.instance.StopHost();
    //     }
    //     else if (NetworkClient.isConnected)
    //     {
    //         DomicileNetworkRoomManager.instance.StopClient();
    //     }
    //     else if (NetworkServer.active)
    //     {
    //         DomicileNetworkRoomManager.instance.StopServer();
    //     }
    // }
}
