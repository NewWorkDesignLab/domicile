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
        if (OnlinePlayer.scenario?.rooms == RoomCount.two)
            Application.OpenURL("https://tobiasbohn.com/domcl/FA-3m_-A-I_2-Zimmer_Atzenbeck.pdf");
        else if (OnlinePlayer.scenario?.rooms == RoomCount.three)
            Application.OpenURL("https://tobiasbohn.com/domcl/FA-2w_-A-I_3-Zimmer_Gebhard.pdf");
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
