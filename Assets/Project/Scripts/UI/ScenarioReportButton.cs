using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class ScenarioReportButton : MonoBehaviour
{
    public ButtonManagerBasicWithIcon button;

    void OnEnable()
    {
        StartCoroutine(UpdateVisuals());
    }

    public void OpenReport()
    {
        var rooms = SessionInstance.instance.session.rooms == RoomCount.two ? 2 : 3;
        var gender = SessionInstance.instance.session.scenarioGender == Gender.divers ? "m" : null;
        gender = SessionInstance.instance.session.scenarioGender == Gender.male ? "m" : gender;
        gender = SessionInstance.instance.session.scenarioGender == Gender.female ? "w" : gender;

        var link = string.Format("https://www.domicile-vr.de/webinterface/{0}_{1}-Zimmer_{2}.pdf", gender, rooms, SessionInstance.instance.session.randomDocumentNumber);
        Application.OpenURL(link);
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
