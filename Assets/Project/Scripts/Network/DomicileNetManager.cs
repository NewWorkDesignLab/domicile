using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using UnityEngine.Networking;

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

    /// <summary>
    /// Called on the client when connected to a server.
    /// <para>The default implementation of this function sets the client as ready and adds a player. Override the function to dictate what happens when the client connects.</para>
    /// </summary>
    /// <param name="conn">Connection to the server.</param>
    public override void OnClientConnect()
    {
        base.OnClientConnect();

        CreatePlayerMessage createPlayerMessage = new CreatePlayerMessage {
            name = SessionInstance.instance.session.name,
            role = SessionInstance.instance.session.localRole,
            gender = SessionInstance.instance.session.gender,
            target = SessionInstance.instance.session.target,
            scenario = SessionInstance.instance.session.scenario,
            scenarioName = SessionInstance.instance.session.scenarioName,
            rooms = SessionInstance.instance.session.rooms,
            textures = SessionInstance.instance.session.textures,
            difficulty = SessionInstance.instance.session.difficulty,
            report = SessionInstance.instance.session.report,
            tenant = SessionInstance.instance.session.tenant,
            contract = SessionInstance.instance.session.contract,
            protocol = SessionInstance.instance.session.protocol
        };

        NetworkClient.connection.Send (createPlayerMessage);
    }

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

    void OnCreatePlayer (NetworkConnectionToClient conn, CreatePlayerMessage message)
    {
        StartCoroutine(OnCreatePlayerDelayed(conn, message));
    }

    IEnumerator OnCreatePlayerDelayed(NetworkConnectionToClient conn, CreatePlayerMessage message)
    {
        // wait for server to async load all subscenes for game instances
        while (!scenePoolLoaded)
            yield return null;

        // get existing scenario from pool
        ScenePoolItem targetScene = scenePool.Get(message.scenario);

        // create new scenario if not existing jet
        bool newScenario = false;
        if (targetScene == null && message.target == SessionTarget.create)
        {
            targetScene = scenePool.GetUnused();
            newScenario = true;
        }

        if (targetScene?.scene == null)
        {
            Debug.LogWarning("REQUESTED SCENE NOT FOUND OR NO SCENE AVAILABLE");
            yield break;
        }

        yield return new WaitForSecondsRealtime(1f);
        // yield return new WaitForEndOfFrame();

        // Send Scene message to client to additively load the game scene
        conn.Send(new SceneMessage { sceneName = targetScene.scene.path, sceneOperation = SceneOperation.LoadAdditive });

        // Wait for end of frame before adding the player to ensure Scene Message goes first
        // Wait a bit longer than a Frame because of Behaviour that CreatePlayer is ignored if Scene not loaded
        // yield return new WaitForEndOfFrame();
        yield return new WaitForSecondsRealtime(1f);

        // create player
        GameObject playerGo = Instantiate (playerPrefab);
        OnlinePlayer player = playerGo.GetComponent<OnlinePlayer> ();
        player.playerName = message.name;
        player.playerGender = message.gender;
        player.playerRole = message.role;
        player.playerTarget = message.target;


        // Do this only on server, not on clients
        // This is what allows the NetworkSceneChecker on player and scene objects
        // to isolate matches per scene instance on server.
        SceneManager.MoveGameObjectToScene(playerGo, targetScene.scene);

        NetworkServer.Spawn (playerGo);
        NetworkServer.AddPlayerForConnection (conn, playerGo);

        // create synced scenario
        if (newScenario)
        {
            GameObject scenarioGo = Instantiate (networkedScenarioPrefab);
            NetworkedScenario netScenario = scenarioGo.GetComponent<NetworkedScenario> ();
            netScenario.scenarioID = targetScene.id;
            netScenario.scenarioName = message.scenarioName;
            netScenario.scenarioGender = message.gender;
            netScenario.scenarioRandomDocumentNumber = UnityEngine.Random.Range(1, 10);
            netScenario.rooms = message.rooms;
            netScenario.textures = message.textures;
            netScenario.difficulty = message.difficulty;
            netScenario.report = message.report;
            netScenario.tenant = message.tenant;
            netScenario.contract = message.contract;
            netScenario.protocol = message.protocol;

            // Do this only on server, not on clients
            // This is what allows the NetworkSceneChecker on player and scene objects
            // to isolate matches per scene instance on server.
            SceneManager.MoveGameObjectToScene(scenarioGo, targetScene.scene);
            targetScene.scenario = netScenario;

            NetworkServer.Spawn (scenarioGo);
        }

        // update the scenario gender wit this gender if this is a tenant
        if (targetScene.scenario != null && message.role == PlayerRole.guide) {
            targetScene.scenario.scenarioGender = message.gender;
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
}
