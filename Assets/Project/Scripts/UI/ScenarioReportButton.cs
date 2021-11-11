using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class ScenarioReportButton : MonoBehaviour
{
    public ButtonManagerBasic button;

    public void OpenReport()
    {
        Application.OpenURL("https://tobiasbohn.com/particle-rush/tobias_bohn_particle_rush_dokumentation_umsetzung.pdf");
    }

    public void UpdateVisuals()
    {
        string reportTxt = TextGenerator.GenerateReportText(
            NetworkedScenario.instance.tenant,
            NetworkedScenario.instance.contract,
            NetworkedScenario.instance.protocol
        );
        button.buttonText = reportTxt;
        button.UpdateUI();
    }
}
