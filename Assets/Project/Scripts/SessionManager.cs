using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : Singleton<SessionManager>
{
    public void PrintSession()
    {
        Debug.Log(SessionInstance.instance.session.ToString());
    }

    public void ClearSession()
    {
        SessionInstance.instance.ClearSession();
    }

    public void SetSessionName(string value)
    {
        SessionInstance.instance.session.name = value;
    }

    public void SetSessionGender(int value)
    {
        SessionInstance.instance.session.gender = (Gender)value;
    }

    public void SetSessionConsent(bool value)
    {
        SessionInstance.instance.session.consent = value;
    }

    public void SetSessionRole(int value)
    {
        SessionInstance.instance.session.localRole = (PlayerRole)value;
    }

    public void SetSessionTarget(int value)
    {
        SessionInstance.instance.session.target = (SessionTarget)value;
    }

    public void SetSessionScenario(string value)
    {
        SessionInstance.instance.session.scenario = value;
    }

    public void SetSessionScenarioName(string value)
    {
        SessionInstance.instance.session.scenarioName = value == "" ? "Scenario" : value;
    }

    public void SetSessionRooms(int value)
    {
        SessionInstance.instance.session.rooms = (RoomCount)value;
    }

    public void SetSessionTextures(int value)
    {
        SessionInstance.instance.session.textures = (TextureDifficulty)value;
    }

    public void SetSessionDifficulty(float value)
    {
        SessionInstance.instance.session.difficulty = value;
    }

    public void SetSessionReport(int value)
    {
        SessionInstance.instance.session.report = (CaseReport)value;
    }

    public void SetSessionTenant(int value)
    {
        SessionInstance.instance.session.tenant = (Tenant)value;
    }

    public void SetSessionContract(int value)
    {
        SessionInstance.instance.session.contract = (RentalContract)value;
    }

    public void SetSessionProtocol(int value)
    {
        SessionInstance.instance.session.protocol = (HandoverProtocol)value;
    }
}
