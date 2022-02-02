using Mirror;

public struct CreatePlayerMessage : NetworkMessage {
    public string name;
    public PlayerRole role;
    public Gender gender;
    public SessionTarget target;
    public string scenario;
    public string scenarioName;

    public RoomCount rooms;
    public TextureDifficulty textures;
    public float difficulty;
    public CaseReport report;
    public Tenant tenant;
    public RentalContract contract;
    public HandoverProtocol protocol;
}
