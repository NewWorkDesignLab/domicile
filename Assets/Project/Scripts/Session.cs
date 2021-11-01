using System;
using UnityEngine;

public class Session
{
    public string name = "";
    public bool consent = false;
    public SessionTarget target = SessionTarget.unspecified;
    public bool setup = false;
    public int scenario = 0;

    public RoomCount rooms = RoomCount.two;
    public TextureDifficulty textures = TextureDifficulty.medium;
    public CaseReport report = CaseReport.auto;
    public Tenant tenant = Tenant.one;
    public RentalContract contract = RentalContract.one;
    public HandoverProtocol protocol = HandoverProtocol.one;

    public Session() {}

    public void Clear()
    {
        name = "";
        consent = false;
        target = SessionTarget.unspecified;
        setup = false;
        scenario = 0;
    }

    public bool IsValidName() {
        return this.name.Length >= 3;
    }

    public string ToString() {
        return String.Format("Name: \"{0}\" | Consent: {1} | Target: {2} | Setup: {3}\nRooms: {4} | Textures: {5} | Report: {6} | Tenant: {7} | Contract: {8} | Protocol: {9}", name, consent, target, setup, rooms, textures, report, tenant, contract, protocol);
    }
}

public enum SessionTarget {unspecified = 0, create = 1, join = 2};

public enum RoomCount {two = 2, three = 3}
public enum TextureDifficulty {easy = 0, medium = 1, hard = 2}
public enum CaseReport {auto = 0, manual = 1}

public enum Tenant {one = 0, two = 1, three = 2}
public enum RentalContract {one = 0, two = 1, three = 2}
public enum HandoverProtocol {one = 0, two = 1, three = 2}
