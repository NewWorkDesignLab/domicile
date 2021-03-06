using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScenarioNameDisplay : MonoBehaviour
{
    public TMP_Text scenarioName;

    void OnEnable()
    {
        StartCoroutine(UpdateVisuals());
    }
    
    private IEnumerator UpdateVisuals()
    {
        while (OnlinePlayer.scenario == null)
            yield return null;

        scenarioName.text = OnlinePlayer.scenario.scenarioName;
    }
}
