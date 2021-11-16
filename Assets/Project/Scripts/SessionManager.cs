using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : Singleton<SessionManager>
{
    public Session session;
    
    protected override void Awake()
    {
        base.Awake();
        session = new Session();
    }

    public void PrintSession()
    {
        Debug.Log(session.ToString());
    }

    public void SetSessionName(string value)
    {
        session.name = value;
    }

    public void SetSessionGender(int value)
    {
        session.gender = (Gender)value;
    }

    public void SetSessionConsent(bool value)
    {
        session.consent = value;
    }

    public void SetSessionTarget(int value)
    {
        session.target = (SessionTarget)value;
    }

    public void SetSessionScenario(string value)
    {
        session.scenario = value;
    }

    public void SetSessionScenarioName(string value)
    {
        session.scenarioName = value == "" ? "Scenario" : value;
    }

    public void SetSessionRooms(int value)
    {
        session.rooms = (RoomCount)value;
    }

    public void SetSessionTextures(int value)
    {
        session.textures = (TextureDifficulty)value;
    }

    public void SetSessionReport(int value)
    {
        session.report = (CaseReport)value;
    }

    public void SetSessionTenant(int value)
    {
        session.tenant = (Tenant)value;
    }

    public void SetSessionContract(int value)
    {
        session.contract = (RentalContract)value;
    }

    public void SetSessionProtocol(int value)
    {
        session.protocol = (HandoverProtocol)value;
    }
}
