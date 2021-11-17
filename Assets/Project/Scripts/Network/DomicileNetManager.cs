using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

public class DomicileNetManager : NetworkManager
{
    [Header("MultiScene Setup")]
    [Scene] public string gameScene;
    public int instances = 20;

    public GameObject networkedScenarioPrefab;

    // This is set true after server loads all subscene instances
    private bool scenePoolLoaded;

    // subscenes are added to this pool as they're loaded
    private ScenePool scenePool = new ScenePool();

    #region Unity Callbacks

    public override void OnValidate()
    {
        base.OnValidate();
    }

    /// <summary>
    /// Runs on both Server and Client
    /// Networking is NOT initialized when this fires
    /// </summary>
    public override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// Runs on both Server and Client
    /// Networking is NOT initialized when this fires
    /// </summary>
    public override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Runs on both Server and Client
    /// </summary>
    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    /// <summary>
    /// Runs on both Server and Client
    /// </summary>
    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    #endregion

    #region Start & Stop

    /// <summary>
    /// Set the frame rate for a headless server.
    /// <para>Override if you wish to disable the behavior or set your own tick rate.</para>
    /// </summary>
    public override void ConfigureHeadlessFrameRate()
    {
        base.ConfigureHeadlessFrameRate();
    }

    /// <summary>
    /// called when quitting the application by closing the window / pressing stop in the editor
    /// </summary>
    public override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
    }

    #endregion

    #region Scene Management

    /// <summary>
    /// This causes the server to switch scenes and sets the networkSceneName.
    /// <para>Clients that connect to this server will automatically switch to this scene. This is called automatically if onlineScene or offlineScene are set, but it can be called from user code to switch scenes again while the game is in progress. This automatically sets clients to be not-ready. The clients must call NetworkClient.Ready() again to participate in the new scene.</para>
    /// </summary>
    /// <param name="newSceneName"></param>
    public override void ServerChangeScene(string newSceneName)
    {
        base.ServerChangeScene(newSceneName);
    }

    /// <summary>
    /// Called from ServerChangeScene immediately before SceneManager.LoadSceneAsync is executed
    /// <para>This allows server to do work / cleanup / prep before the scene changes.</para>
    /// </summary>
    /// <param name="newSceneName">Name of the scene that's about to be loaded</param>
    public override void OnServerChangeScene(string newSceneName) { }

    /// <summary>
    /// Called on the server when a scene is completed loaded, when the scene load was initiated by the server with ServerChangeScene().
    /// </summary>
    /// <param name="sceneName">The name of the new scene.</param>
    public override void OnServerSceneChanged(string sceneName) { }

    /// <summary>
    /// Called from ClientChangeScene immediately before SceneManager.LoadSceneAsync is executed
    /// <para>This allows client to do work / cleanup / prep before the scene changes.</para>
    /// </summary>
    /// <param name="newSceneName">Name of the scene that's about to be loaded</param>
    /// <param name="sceneOperation">Scene operation that's about to happen</param>
    /// <param name="customHandling">true to indicate that scene loading will be handled through overrides</param>
    public override void OnClientChangeScene(string newSceneName, SceneOperation sceneOperation, bool customHandling) { }

    /// <summary>
    /// Called on clients when a scene has completed loaded, when the scene load was initiated by the server.
    /// <para>Scene changes can cause player objects to be destroyed. The default implementation of OnClientSceneChanged in the NetworkManager is to add a player object for the connection if no player object exists.</para>
    /// </summary>
    /// <param name="conn">The network connection that the scene change message arrived on.</param>
    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);
    }

    #endregion

    #region Server System Callbacks

    /// <summary>
    /// Called on the server when a new client connects.
    /// <para>Unity calls this on the Server when a Client connects to the Server. Use an override to tell the NetworkManager what to do when a client connects to the server.</para>
    /// </summary>
    /// <param name="conn">Connection from client.</param>
    public override void OnServerConnect(NetworkConnection conn) { }

    /// <summary>
    /// Called on the server when a client is ready.
    /// <para>The default implementation of this function calls NetworkServer.SetClientReady() to continue the network setup process.</para>
    /// </summary>
    /// <param name="conn">Connection from client.</param>
    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);
    }

    /// <summary>
    /// Called on the server when a client adds a new player with ClientScene.AddPlayer.
    /// <para>The default implementation for this function creates a new player object from the playerPrefab.</para>
    /// </summary>
    /// <param name="conn">Connection from client.</param>
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
    }

    /// <summary>
    /// Called on the server when a client disconnects.
    /// <para>This is called on the Server when a Client disconnects from the Server. Use an override to decide what should happen when a disconnection is detected.</para>
    /// </summary>
    /// <param name="conn">Connection from client.</param>
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
    }

    /// <summary>
    /// Called on server when transport raises an exception.
    /// <para>NetworkConnection may be null.</para>
    /// </summary>
    /// <param name="conn">Connection of the client...may be null</param>
    /// <param name="exception">Exception thrown from the Transport.</param>
    public override void OnServerError(NetworkConnection conn, Exception exception) { }

    #endregion

    #region Client System Callbacks

    /// <summary>
    /// Called on the client when connected to a server.
    /// <para>The default implementation of this function sets the client as ready and adds a player. Override the function to dictate what happens when the client connects.</para>
    /// </summary>
    /// <param name="conn">Connection to the server.</param>
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        CreatePlayerMessage createPlayerMessage = new CreatePlayerMessage {
            name = SessionManager.instance.session.name,
            role = PlayerRole.spectator,
            gender = SessionManager.instance.session.gender,
            target = SessionManager.instance.session.target,
            scenario = SessionManager.instance.session.scenario,
            scenarioName = SessionManager.instance.session.scenarioName,
            rooms = SessionManager.instance.session.rooms,
            textures = SessionManager.instance.session.textures,
            report = SessionManager.instance.session.report,
            tenant = SessionManager.instance.session.tenant,
            contract = SessionManager.instance.session.contract,
            protocol = SessionManager.instance.session.protocol
        };

        conn.Send (createPlayerMessage);
    }

    /// <summary>
    /// Called on clients when disconnected from a server.
    /// <para>This is called on the client when it disconnects from the server. Override this function to decide what happens when the client disconnects.</para>
    /// </summary>
    /// <param name="conn">Connection to the server.</param>
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
    }

    /// <summary>
    /// Called on clients when a servers tells the client it is no longer ready.
    /// <para>This is commonly used when switching scenes.</para>
    /// </summary>
    /// <param name="conn">Connection to the server.</param>
    public override void OnClientNotReady(NetworkConnection conn) { }

    /// <summary>
    /// Called on client when transport raises an exception.</summary>
    /// </summary>
    /// <param name="exception">Exception thrown from the Transport.</param>
    public override void OnClientError(Exception exception) { }

    #endregion

    #region Start & Stop Callbacks

    // Since there are multiple versions of StartServer, StartClient and StartHost, to reliably customize
    // their functionality, users would need override all the versions. Instead these callbacks are invoked
    // from all versions, so users only need to implement this one case.

    /// <summary>
    /// This is invoked when a host is started.
    /// <para>StartHost has multiple signatures, but they all cause this hook to be called.</para>
    /// </summary>
    public override void OnStartHost() { }

    /// <summary>
    /// This is invoked when a server is started - including when a host is started.
    /// <para>StartServer has multiple signatures, but they all cause this hook to be called.</para>
    /// </summary>
    public override void OnStartServer()
    {
        base.OnStartServer ();
        NetworkServer.RegisterHandler<CreatePlayerMessage> (OnCreatePlayer);
        StartCoroutine(ServerLoadSubScenes());
    }

    void OnCreatePlayer (NetworkConnection conn, CreatePlayerMessage message)
    {
        StartCoroutine(OnCreatePlayerDelayed(conn, message));
    }

    IEnumerator OnCreatePlayerDelayed(NetworkConnection conn, CreatePlayerMessage message)
    {
        // wait for server to async load all subscenes for game instances
        while (!scenePoolLoaded)
            yield return null;

        // get or find scene from pool
        ScenePoolItem targetScene = message.target switch
        {
            SessionTarget.unspecified => null,
            SessionTarget.create => scenePool.GetUnused(),
            SessionTarget.join => scenePool.Get(message.scenario),
            SessionTarget.offline => null,
            _ => null
        };
        
        if (targetScene == null)
        {
            Debug.LogWarning("REQUESTED SCENE NOT FOUND OR NO SCENE AVAILABLE");
            yield break;
        }

        // Send Scene message to client to additively load the game scene
        conn.Send(new SceneMessage { sceneName = gameScene, sceneOperation = SceneOperation.LoadAdditive });

        // Wait for end of frame before adding the player to ensure Scene Message goes first
        yield return new WaitForEndOfFrame();

        // create player
        GameObject playerGo = Instantiate (playerPrefab);
        OnlinePlayer player = playerGo.GetComponent<OnlinePlayer> ();
        player.playerName = message.name;
        player.playerGender = message.gender;
        player.playerRole = message.role;
        player.playerTarget = message.target;
        NetworkServer.Spawn (playerGo);
        NetworkServer.AddPlayerForConnection (conn, playerGo);

        // Do this only on server, not on clients
        // This is what allows the NetworkSceneChecker on player and scene objects
        // to isolate matches per scene instance on server.
        SceneManager.MoveGameObjectToScene(playerGo, targetScene.scene);

        // create synced scenario
        if (message.target == SessionTarget.create)
        {
            GameObject scenarioGo = Instantiate (networkedScenarioPrefab);
            NetworkedScenario netScenario = scenarioGo.GetComponent<NetworkedScenario> ();
            netScenario.scenarioID = targetScene.id;
            netScenario.scenarioName = message.scenarioName;
            netScenario.rooms = message.rooms;
            netScenario.textures = message.textures;
            netScenario.report = message.report;
            netScenario.tenant = message.tenant;
            netScenario.contract = message.contract;
            netScenario.protocol = message.protocol;
            NetworkServer.Spawn (scenarioGo);
            
            // Do this only on server, not on clients
            // This is what allows the NetworkSceneChecker on player and scene objects
            // to isolate matches per scene instance on server.
            SceneManager.MoveGameObjectToScene(scenarioGo, targetScene.scene);
        }
    }

    // We're additively loading scenes, so GetSceneAt(0) will return the main "container" scene,
    // therefore we start the index at one and loop through instances value inclusively.
    // If instances is zero, the loop is bypassed entirely.
    IEnumerator ServerLoadSubScenes()
    {
        for (int index = 1; index <= instances; index++)
        {
            yield return SceneManager.LoadSceneAsync(gameScene, LoadSceneMode.Additive);

            Scene newScene = SceneManager.GetSceneAt(index);
            scenePool.Add(newScene);
        }

        scenePoolLoaded = true;
    }

    /// <summary>
    /// This is invoked when the client is started.
    /// </summary>
    public override void OnStartClient() { }

    /// <summary>
    /// This is called when a host is stopped.
    /// </summary>
    public override void OnStopHost() { }

    /// <summary>
    /// This is called when a server is stopped - including when a host is stopped.
    /// </summary>
    public override void OnStopServer()
    {
        NetworkServer.SendToAll(new SceneMessage { sceneName = gameScene, sceneOperation = SceneOperation.UnloadAdditive });
        StartCoroutine(ServerUnloadSubScenes());
    }

    // Unload the subScenes and unused assets and clear the subScenes list.
    IEnumerator ServerUnloadSubScenes()
    {
        for (int index = 0; index < scenePool.Count; index++)
            yield return SceneManager.UnloadSceneAsync(scenePool.Get(index).scene);

        scenePool.Clear();
        scenePoolLoaded = false;

        yield return Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// This is called when a client is stopped.
    /// </summary>
    public override void OnStopClient()
    {
        // make sure we're not in host mode
        if (mode == NetworkManagerMode.ClientOnly)
            StartCoroutine(ClientUnloadSubScenes());
    }

    // Unload all but the active scene, which is the "container" scene
    IEnumerator ClientUnloadSubScenes()
    {
        for (int index = 0; index < SceneManager.sceneCount; index++)
        {
            if (SceneManager.GetSceneAt(index) != SceneManager.GetActiveScene())
                yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(index));
        }
    }

    #endregion
}
