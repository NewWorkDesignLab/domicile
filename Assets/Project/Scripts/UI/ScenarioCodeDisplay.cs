using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScenarioCodeDisplay : MonoBehaviour
{
    public TMP_Text scenarioCode;

    void OnEnable()
    {
        StartCoroutine(UpdateVisuals());
    }
    
    private IEnumerator UpdateVisuals()
    {
        while (OnlinePlayer.scenario == null)
            yield return null;

        scenarioCode.text = $"Raum-Code: {OnlinePlayer.scenario.scenarioID}";
    }
}
