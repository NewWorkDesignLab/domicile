using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextureDefinition : System.ICloneable
{
    public string id;
    public string name;
    public TextureType type;
    public TextureDifficulty difficulty;
    public TextureVariant[] placements;
    [HideInInspector] public bool alreadyUsed = false;

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public override string ToString()
    {
        string retStrn = System.String.Format("Texture {0} with difficulty {1}", id, difficulty);
        return retStrn;
    }

    public float GetFloatDiffuculty()
    {
        float _ret = difficulty switch
        {
            TextureDifficulty.easy => 1f,
            TextureDifficulty.medium => 1.5f,
            TextureDifficulty.hard => 2f,
            _ => 1.5f
        };
        return _ret;
    }

    public TextureVariant GetRandomTextureVariant()
    {
        if (placements.Length <= 0)
            return null;

        int maxIterations = 10;
        while (maxIterations >= 0)
        {
            maxIterations--;
            int randomVariantIndex = Random.Range(0, placements.Length);
            if (placements[randomVariantIndex] != null) {
                bool variantIsInKizi = placements[randomVariantIndex].placementLocation == Location.Kinderzimmer;
                bool kiziIncluded = SessionInstance.instance.session.rooms == RoomCount.three;
                if ((variantIsInKizi && kiziIncluded) || !variantIsInKizi)
                {
                    return placements[randomVariantIndex];
                }
            }
        }
        return null;
    }
}