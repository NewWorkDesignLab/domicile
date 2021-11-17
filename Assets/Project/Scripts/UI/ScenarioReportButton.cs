using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class ScenarioReportButton : MonoBehaviour
{
    public ButtonManagerBasic button;

    void OnEnable()
    {
        StartCoroutine(UpdateVisuals());
    }

    public void OpenReport()
    {
        Application.OpenURL("https://tobiasbohn.com/particle-rush/tobias_bohn_particle_rush_dokumentation_umsetzung.pdf");
    }

    private IEnumerator UpdateVisuals()
    {
        while (OnlinePlayer.scenario == null)
            yield return null;
            
        string reportTxt = TextGenerator.GenerateReportText(
            OnlinePlayer.scenario.tenant,
            OnlinePlayer.scenario.contract,
            OnlinePlayer.scenario.protocol
        );
        button.buttonText = reportTxt;
        button.UpdateUI();
    }
}
