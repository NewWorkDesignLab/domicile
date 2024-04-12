using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/guides/networkbehaviour
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

// NOTE: Do not put objects in DontDestroyOnLoad (DDOL) in Awake.  You can do that in Start instead.

public class OnlinePlayer : NetworkBehaviour
{
    public static OnlinePlayer localPlayer;
    public static OnlinePlayer followPlayer;
    public static NetworkedScenario scenario;

    private static List<OnlinePlayer> playersInScene = new List<OnlinePlayer>();
    public bool roomReadyState = false;

    [Header("Base Player Reference")]
    public BasePlayer player;

    [Header("Lobby UI Reference")]
    public GameObject lobbyPlayerUIPrefab;
    private GameObject instanciatedLobbyPlayerUI;

    #region Hooks

    public event System.Action<bool> OnReadyStateChanged;
    public event System.Action<PlayerRole> OnRoleChanged;
    public event System.Action<bool> OnRoomReadyStateChanged;

    #endregion

    #region Sync Vars & Hooks

    [Header("Sync Vars")]
    [SyncVar] public string playerName;
    [SyncVar] public Gender playerGender;
    [SyncVar] public SessionTarget playerTarget;

    [SyncVar(hook = nameof(HookRoleChanged))]
    public PlayerRole playerRole;

    [SyncVar(hook = nameof(HookReadyStateChanged))]
    public bool playerReady;

    private void HookRoleChanged(PlayerRole _, PlayerRole newValue)
    {
        OnRoleChanged?.Invoke(newValue);
        if (newValue == PlayerRole.learner)
            OnlinePlayer.followPlayer = this;
    }

    private void HookReadyStateChanged(bool _, bool newValue)
    {
        OnReadyStateChanged?.Invoke(newValue);
        if (localPlayer != null) localPlayer.CheckAllPlayerReady();
    }

    #endregion

    #region Commands

    [Command]
    public void CmdSetRole(PlayerRole value)
    {
        // change the actual role value
        playerRole = value;

        // find all online playes in this lobby
        OnlinePlayer[] list = GameObject.FindObjectsByType<OnlinePlayer>(FindObjectsSortMode.None);
        // iterate all and return the gender of the first tenant
        foreach (OnlinePlayer item in list)
        {
            if (item.playerRole == PlayerRole.guide) {
                // as we are on the server, te scenario variable is not present
                // search for the current networked scenario to set the scenario gender
                NetworkedScenario scen = GameObject.FindObjectOfType<NetworkedScenario>();
                // update the scenario gender to the gende rof this first tenant
                if (scen != null) scen.scenarioGender = item.playerGender;
                // dont care about all other tenants if there are multiple in this lobby
                break;
            }
        }
    }

    [Command]
    public void CmdSetReadyState(bool value)
    {
        playerReady = value;
    }

    #endregion

    #region Start & Stop Callbacks

    /// <summary>
    /// This is invoked for NetworkBehaviour objects when they become active on the server.
    /// <para>This could be triggered by NetworkServer.Listen() for objects in the scene, or by NetworkServer.Spawn() for objects that are dynamically created.</para>
    /// <para>This will be called for objects on a "host" as well as for object on a dedicated server.</para>
    /// </summary>
    public override void OnStartServer()
    {
        Debug.Log ("[InactivePlayer Start] A new Player joined to Server: " + playerName + " - " + playerTarget + " - " + playerRole);
        player.SetupInactivePlayer ();
    }

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
        // add to list of players in scene
        playersInScene.Add(this);
        // OnStartLocalPlayer called after OnStartClient - static localPlayer var could be null
        if (localPlayer != null) localPlayer.CheckAllPlayerReady();

        // setup all clients as inactive
        player.SetupInactivePlayer ();

        // instantiate lobby UI
        instanciatedLobbyPlayerUI = Instantiate(lobbyPlayerUIPrefab, LobbyUIManager.instance.lobbyPlayerUIParent.transform);

        // Set this player object in PlayerUI to wire up event handlers
        instanciatedLobbyPlayerUI.GetComponent<LobbyPlayerUI>().SetPlayer(this);

        // Invoke all event handlers with the current data
        OnRoleChanged?.Invoke(playerRole);
        OnReadyStateChanged?.Invoke(playerReady);
    }

    /// <summary>
    /// This is invoked on clients when the server has caused this object to be destroyed.
    /// <para>This can be used as a hook to invoke effects or do client specific cleanup.</para>
    /// </summary>
    public override void OnStopClient()
    {
        // remove from list of players in scene
        playersInScene.Remove(this);
        localPlayer.CheckAllPlayerReady();

        // Remove this player's UI object
        Destroy(instanciatedLobbyPlayerUI);
    }

    /// <summary>
    /// Called when the local player object has been set up.
    /// <para>This happens after OnStartClient(), as it is triggered by an ownership message from the server. This is an appropriate place to activate components or functionality that should only be active for the local player, such as cameras and input.</para>
    /// </summary>
    public override void OnStartLocalPlayer()
    {
        localPlayer = this;
        player.SetupLocalPlayer ();

        // set ready State, ReadyStateHook will check for complete Room Ready State
        if (playerTarget == SessionTarget.create) CmdSetReadyState(true);

        // enable UI when localPlayer is complete setup
        SyncSceneProgress.instance.ApplySceneStatus();

        if (scenario != null) UpdateSessionWithNetworkedScenario();
    }

    public void UpdateSessionWithNetworkedScenario()
    {
        // set values from networked scenario in session
        SessionInstance.instance.session.scenario = scenario.scenarioID;
        SessionInstance.instance.session.scenarioName = scenario.scenarioName;
        SessionInstance.instance.session.scenarioGender = scenario.scenarioGender;
        SessionInstance.instance.session.randomDocumentNumber = scenario.scenarioRandomDocumentNumber;
        SessionInstance.instance.session.rooms = scenario.rooms;
        SessionInstance.instance.session.textures = scenario.textures;
        SessionInstance.instance.session.difficulty = scenario.difficulty;
        SessionInstance.instance.session.report = scenario.report;
        SessionInstance.instance.session.tenant = scenario.tenant;
        SessionInstance.instance.session.contract = scenario.contract;
        SessionInstance.instance.session.protocol = scenario.protocol;
    }

    /// <summary>
    /// This is invoked on behaviours that have authority, based on context and <see cref="NetworkIdentity.hasAuthority">NetworkIdentity.hasAuthority</see>.
    /// <para>This is called after <see cref="OnStartServer">OnStartServer</see> and before <see cref="OnStartClient">OnStartClient.</see></para>
    /// <para>When <see cref="NetworkIdentity.AssignClientAuthority">AssignClientAuthority</see> is called on the server, this will be called on the client that owns the object. When an object is spawned with <see cref="NetworkServer.Spawn">NetworkServer.Spawn</see> with a NetworkConnection parameter included, this will be called on the client that owns the object.</para>
    /// </summary>
    public override void OnStartAuthority() { }

    /// <summary>
    /// This is invoked on behaviours when authority is removed.
    /// <para>When NetworkIdentity.RemoveClientAuthority is called on the server, this will be called on the client that owns the object.</para>
    /// </summary>
    public override void OnStopAuthority() { }

    #endregion


    public void LeaveLobby()
    {
        SessionInstance.instance.ClearSession();
        if (NetworkServer.active && NetworkClient.isConnected)
            NetworkManager.singleton.StopHost();
        else if (NetworkClient.isConnected)
            NetworkManager.singleton.StopClient();
        else if (NetworkServer.active)
            NetworkManager.singleton.StopServer();
    }

    private void CheckAllPlayerReady()
    {
        bool allPlayersReady = true;
        for (int i = 0; i < playersInScene.Count; i++)
        {
            if (!playersInScene[i].playerReady)
            {
                allPlayersReady = false;
                break;
            }
        }

        if (allPlayersReady != roomReadyState) {
            roomReadyState = allPlayersReady;
            OnRoomReadyStateChanged?.Invoke(roomReadyState);
        }
    }

    public void SetupClientVisabillity()
    {
        for (int i = 0; i < playersInScene.Count; i++)
        {
            if (playersInScene[i].isLocalPlayer)
                playersInScene[i].player.SetupLocalPlayer();
            else if (playersInScene[i].playerRole == PlayerRole.learner)
                playersInScene[i].player.SetupVisablePlayer(true);
            else if (playersInScene[i].playerRole == PlayerRole.guide)
                playersInScene[i].player.SetupVisablePlayer(true);
            else
                playersInScene[i].player.SetupInactivePlayer();
        }
    }
}
