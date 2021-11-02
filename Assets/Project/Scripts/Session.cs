using System;
using UnityEngine;

public class Session
{
    public string name = "";
    public bool consent = false;
    public Gender gender = Gender.unspecified;
    public SessionTarget target = SessionTarget.unspecified;
    public string scenario = "";
    public string scenarioName = "";

    public RoomCount rooms = RoomCount.two;
    public TextureDifficulty textures = TextureDifficulty.medium;
    public CaseReport report = CaseReport.auto;
    public Tenant tenant = Tenant.one;
    public RentalContract contract = RentalContract.one;
    public HandoverProtocol protocol = HandoverProtocol.one;

    public Session() {}

    public bool IsValidName() {
        return this.name.Length >= 2;
    }

    public string ToString() {
        return String.Format("Name: \"{0}\" | Gender: {11} | Consent: {1} | Target: {2} | Scenario: \"{9}\" | Scenario Name: \"{10}\"\nSETTINGS [Rooms: {3} | Textures: {4} | Report: {5} | Tenant: {6} | Contract: {7} | Protocol: {8}]", name, consent, target, rooms, textures, report, tenant, contract, protocol, scenario, scenarioName, gender);
    }
}