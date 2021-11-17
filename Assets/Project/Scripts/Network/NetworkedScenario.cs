using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkedScenario : NetworkBehaviour
{
    /// <summary>
    /// Called on every NetworkBehaviour when it is activated on a client.
    /// <para>Objects on the host have this function called, as there is a local client on the host. The values of SyncVars on object are guaranteed to be initialized correctly with the latest state from the server when this function is called on the client.</para>
    /// </summary>
    public override void OnStartClient()
    {
        OnlinePlayer.scenario = this;
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
    private void NameChanged(string _, string newValue) {}
    private void TenantChanged(int _, int newValue) {}
    private void ContractChanged(int _, int newValue) {}
    private void ProtocolChanged(int _, int newValue) {}
}
