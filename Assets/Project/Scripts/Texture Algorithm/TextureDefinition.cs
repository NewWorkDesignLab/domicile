using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextureDefinition : MonoBehaviour
{
    public string id;
    public string description;
    public TextureType type;
    public TextureDifficulty difficulty;
    public TextureVariant[] placements;
    [HideInInspector] public bool alreadyUsed = false;

    public override string ToString()
    {
        string retStrn = System.String.Format("Texture {0} ({1})", id, difficulty);
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

            // shuffle to get random Variant
            placements.Shuffle();

            if (placements[0] != null) {
                bool variantIsInKizi = placements[0].placementLocation == Location.Kinderzimmer;
                bool kiziIncluded = SessionInstance.instance.session.rooms == RoomCount.three;
                if ((variantIsInKizi && kiziIncluded) || !variantIsInKizi)
                {
                    return placements[0];
                }
            }
        }
        return null;
    }
}