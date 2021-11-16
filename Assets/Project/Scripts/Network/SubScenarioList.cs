using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class SubScenarioList
{
    readonly List<SubScenario> subScenarios;

    public SubScenarioList()
    {
        subScenarios = new List<SubScenario>();
    }

    public SubScenario GetScenario(string id)
    {
        return null;
    }

    public SubScenario AddScenario(string id, Scene game, Scene lobby)
    {
        SubScenario result = new SubScenario(id, game, lobby);
        subScenarios.Add(result);
        return result;
    }

    public void UnloadScenario()
    {

    }
}

public class SubScenario
{
    public Scene gameScene;
    public Scene lobbyScene;
    public string id;

    public SubScenario(string id, Scene game, Scene lobby)
    {
        this.gameScene = game;
        this.lobbyScene = lobby;
        this.id = id;
    }
}
