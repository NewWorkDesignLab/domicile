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
        if (OnlinePlayer.scenario?.tenant == Tenant.three)
            Application.OpenURL("https://tobiasbohn.com/domcl/FA-3m_-A-I_2-Zimmer_Atzenbeck.pdf");
        else if (OnlinePlayer.scenario?.tenant == Tenant.two)
            Application.OpenURL("https://tobiasbohn.com/domcl/FA-2w_-A-I_3-Zimmer_Gebhard.pdf");
        else if (OnlinePlayer.scenario?.tenant == Tenant.one) {
            if (OnlinePlayer.scenario?.textures == TextureDifficulty.medium) {
                if (OnlinePlayer.localPlayer.playerGender == Gender.male)
                    Application.OpenURL("https://tobiasbohn.com/domcl/FA-1m_A_I.pdf");
                else if (OnlinePlayer.localPlayer.playerGender == Gender.female)
                    Application.OpenURL("https://tobiasbohn.com/domcl/FA-1w_A_I.pdf");
            }
            else if (OnlinePlayer.scenario?.textures == TextureDifficulty.easy) {
                if (OnlinePlayer.localPlayer.playerGender == Gender.male)
                    Application.OpenURL("https://tobiasbohn.com/domcl/FA-1m_A_I_Lebensraume_Hoyerswerda.pdf");
                else if (OnlinePlayer.localPlayer.playerGender == Gender.female)
                    Application.OpenURL("https://tobiasbohn.com/domcl/FA-1w_A_I_Lebensraume_Hoyerswerda.pdf");
            }
        }
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
