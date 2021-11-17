using UnityEngine;
using TMPro;

public class ScenarioNameDisplay : MonoBehaviour
{
    public TMP_Text scenarioName;
    
    public void UpdateVisuals()
    {
        // Scenario Name Display
        scenarioName.text = OnlinePlayer.scenario.scenarioName;
    }
}
