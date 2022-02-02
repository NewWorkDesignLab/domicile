using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DistributionSetting
{
    public int textureCountEasy;
    public int textureCountMiddle;
    public int textureCountHard;

    public int GetSetting(TextureDifficulty textureDifficulty)
    {
        return textureDifficulty switch
        {
            TextureDifficulty.easy => textureCountEasy,
            TextureDifficulty.medium => textureCountMiddle,
            TextureDifficulty.hard => textureCountHard,
            _ => textureCountMiddle
        };
    }
}
