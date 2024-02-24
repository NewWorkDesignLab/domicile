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
        if (OnlinePlayer.localPlayer != null) OnlinePlayer.localPlayer.UpdateSessionWithNetworkedScenario();
    }


    [SyncVar(hook = nameof(IDChanged))]
    public string scenarioID;

    [SyncVar(hook = nameof(NameChanged))]
    public string scenarioName;

    [SyncVar(hook = nameof(GenderChanged))]
    public Gender scenarioGender;

    [SyncVar(hook = nameof(RoomsChanged))]
    public RoomCount rooms;

    [SyncVar(hook = nameof(TexturesChanged))]
    public TextureDifficulty textures;

    [SyncVar(hook = nameof(DifficultyChanged))]
    public float difficulty;

    [SyncVar(hook = nameof(ReportChanged))]
    public CaseReport report;

    [SyncVar(hook = nameof(TenantChanged))]
    public Tenant tenant;

    [SyncVar(hook = nameof(ContractChanged))]
    public RentalContract contract;

    [SyncVar(hook = nameof(ProtocolChanged))]
    public HandoverProtocol protocol;

    private void IDChanged(string _, string newValue) {}
    private void NameChanged(string _, string newValue) {}
    private void GenderChanged(Gender _, Gender newValue) {}
    private void RoomsChanged(RoomCount _, RoomCount newValue) {}
    private void TexturesChanged(TextureDifficulty _, TextureDifficulty newValue) {}
    private void DifficultyChanged(float _, float newValue) {}
    private void ReportChanged(CaseReport _, CaseReport newValue) {}
    private void TenantChanged(Tenant _, Tenant newValue) {}
    private void ContractChanged(RentalContract _, RentalContract newValue) {}
    private void ProtocolChanged(HandoverProtocol _, HandoverProtocol newValue) {}
}
