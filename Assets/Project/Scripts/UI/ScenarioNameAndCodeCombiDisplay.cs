using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScenarioNameAndCodeCombiDisplay : MonoBehaviour
{
    public TMP_Text field;

    void OnEnable()
    {
        StartCoroutine(UpdateVisuals());
    }
    
    private IEnumerator UpdateVisuals()
    {
        while (OnlinePlayer.scenario == null)
            yield return null;

        field.text = $"<b>{OnlinePlayer.scenario.scenarioName}</b> | Raum-Code: <b>{OnlinePlayer.scenario.scenarioID}</b>";
    }
}
