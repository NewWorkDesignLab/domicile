using System;
using UnityEngine;

[Serializable]
public class Session
{
  // There are 9 Versions for each comination available on Server numbered from 1 to 9
    public int randomDocumentNumber;
    public string name = "";
    public bool consent = false;
    public Gender gender = Gender.divers;
    public PlayerRole localRole = PlayerRole.spectator;
    public SessionTarget target = SessionTarget.unspecified;
    public string scenario = "";
    public string scenarioName = "Szenario";
    public Gender scenarioGender = Gender.divers;

    public RoomCount rooms = RoomCount.two;
    public TextureDifficulty textures = TextureDifficulty.medium;
    public float difficulty = 3;
    public CaseReport report = CaseReport.auto;
    public Tenant tenant = Tenant.one;
    public RentalContract contract = RentalContract.one;
    public HandoverProtocol protocol = HandoverProtocol.one;

    public Session() {}

    public bool IsValidName() {
        return this.name.Length >= 2;
    }

    public override string ToString() {
        return String.Format("Name: \"{0}\" | Gender: {11} | Consent: {1} | Target: {2} | Scenario: \"{9}\" | Scenario Name: \"{10}\"\nSETTINGS [Rooms: {3} | Textures: {4} | Difficulty: {12} | Report: {5} | Tenant: {6} | Contract: {7} | Protocol: {8}]", name, consent, target, rooms, textures, report, tenant, contract, protocol, scenario, scenarioName, gender, difficulty);
    }
}
