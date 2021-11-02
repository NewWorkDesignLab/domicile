using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextGenerator
{
    public static string GenerateReportText(Tenant tenant, RentalContract contract, HandoverProtocol protocol)
    {
        string tmp = "Fall-Akte ";
        tmp += tenant switch
        {
            Tenant.one => "1-",
            Tenant.two => "2-",
            Tenant.three => "3-",
            _ => "1-"
        };
        tmp += contract switch
        {
            RentalContract.one => "A-",
            RentalContract.two => "B-",
            RentalContract.three => "C-",
            _ => "A-"
        };
        tmp += protocol switch
        {
            HandoverProtocol.one => "I",
            HandoverProtocol.two => "II",
            HandoverProtocol.three => "III",
            _ => "I"
        };
        return tmp;
    }

    public static string GenerateTextureText(TextureDifficulty textures)
    {
        string tmp = textures switch
        {
            TextureDifficulty.easy => "Leicht",
            TextureDifficulty.medium => "Mittel",
            TextureDifficulty.hard => "Schwer",
            _ => "Mittel"
        };
        return tmp;
    }

    public static string GenerateRoomText(RoomCount rooms)
    {
        string tmp = rooms switch
        {
            RoomCount.two => "2-Zimmer-Wohnung",
            RoomCount.three => "3-Zimmer-Wohnung",
            _ => "2-Zimmer-Wohnung"
        };
        return tmp;
    }

    public static string GenerateRoleText(PlayerRole role)
    {
        string tmp = role switch
        {
            PlayerRole.spectator => "Zuschauer",
            PlayerRole.guide => "Mieter",
            PlayerRole.learner => "Vermieter",
            _ => "Zuschauer"
        };
        return tmp;
    }
}
