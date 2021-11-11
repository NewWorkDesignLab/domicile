using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkedScenario : NetworkBehaviour
{
    #region Singleton

    public static NetworkedScenario instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public ScenarioReportButton[] reports;
    public ScenarioNameDisplay[] names;

    void UpdateReports()
    {
        for (int i = 0; i < reports.Length; i++)
        {
            reports[i].UpdateVisuals();
        }
    }

    void UpdateNames()
    {
        for (int i = 0; i < names.Length; i++)
        {
            names[i].UpdateVisuals();
        }
    }

    [SyncVar(hook = nameof(IDChanged))]
    public string scenarioID;
    
    [SyncVar(hook = nameof(NameChanged))]
    public string scenarioName;

    [SyncVar(hook = nameof(RoomsChanged))]
    public int _rooms;
    public RoomCount rooms
    {
        get { return (RoomCount)_rooms; }
        set { _rooms = (int)value; }
    }

    [SyncVar(hook = nameof(TexturesChanged))]
    public int _textures;
    public TextureDifficulty textures
    {
        get { return (TextureDifficulty)_textures; }
        set { _textures = (int)value; }
    }

    [SyncVar(hook = nameof(ReportChanged))]
    public int _report;
    public CaseReport report
    {
        get { return (CaseReport)_report; }
        set { _report = (int)value; }
    }

    [SyncVar(hook = nameof(TenantChanged))]
    public int _tenant;
    public Tenant tenant
    {
        get { return (Tenant)_tenant; }
        set { _tenant = (int)value; }
    }

    [SyncVar(hook = nameof(ContractChanged))]
    public int _contract;
    public RentalContract contract
    {
        get { return (RentalContract)_contract; }
        set { _contract = (int)value; }
    }

    [SyncVar(hook = nameof(ProtocolChanged))]
    public int _protocol;
    public HandoverProtocol protocol
    {
        get { return (HandoverProtocol)_protocol; }
        set { _protocol = (int)value; }
    }

    private void IDChanged(string _, string newValue) {}
    private void RoomsChanged(int _, int newValue) {}
    private void TexturesChanged(int _, int newValue) {}
    private void ReportChanged(int _, int newValue) {}

    private void NameChanged(string _, string newValue)
    {
        UpdateNames();
    }

    private void TenantChanged(int _, int newValue)
    {
        UpdateReports();
    }
    private void ContractChanged(int _, int newValue)
    {
        UpdateReports();
    }
    private void ProtocolChanged(int _, int newValue)
    {
        UpdateReports();
    }

    public void InitScenario(string _id, string _name, RoomCount _rooms, TextureDifficulty _textures, CaseReport _report, Tenant _tenant, RentalContract _contract, HandoverProtocol _protocol)
    {
        scenarioID = _id;
        scenarioName = _name;
        rooms = _rooms;
        textures = _textures;
        report = _report;
        tenant = _tenant;
        contract = _contract;
        protocol = _protocol;
    }
}
