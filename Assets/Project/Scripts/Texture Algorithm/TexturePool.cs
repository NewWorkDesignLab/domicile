using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TexturePool : Singleton<TexturePool>
{
    public TextureDefinition[] ASR;
    public TextureDefinition[] AOB;
    public TextureDefinition[] SVN;
    public TextureDefinition[] VSC;
    public TextureDefinition[] OBJ;

    public TextureDefinition GetTexture(TextureType type, TextureDifficulty difficulty, bool checkDifficulty = true, bool allowFallbackWithoutDifficulty = true)
    {
        TextureDefinition[] pool = GetPool(type);

        // shuffle to get random Texture
        pool.Shuffle();

        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].alreadyUsed) continue;

            if (!checkDifficulty || pool[i].difficulty == difficulty)
            {
                pool[i].alreadyUsed = true;
                return pool[i].Clone() as TextureDefinition;
            }
        }

        // no texture found
        if (checkDifficulty && allowFallbackWithoutDifficulty)
        {
            // try again and ignore difficulty
            return GetTexture(type, difficulty, false);
        }
        else
        {
            // no texture available that is not used
            Debug.LogWarning("No Texture found.");
            return null;
        }
    }

    public TextureDefinition TrySwapTexture(TextureDefinition clone, float difference)
    {
        // return if already best choice in difficulty
        if (difference > 0 && clone.difficulty == TextureDifficulty.easy) return null;
        if (difference < 0 && clone.difficulty == TextureDifficulty.hard) return null;

        // get texture pool and randomize order
        TextureDefinition[] pool = GetPool(clone.type);
        pool.Shuffle();

        // input TextureDefinition is just a Clone, but we need the original one
        TextureDefinition original = FindOriginalInPool(clone);

        // find new texture with adjusted difficulty
        TextureDifficulty targetLevel = difference > 0 ? (TextureDifficulty)((int)clone.difficulty - 1) : (TextureDifficulty)((int)clone.difficulty + 1);
        TextureDefinition swapped = GetTexture(clone.type, targetLevel, true, false);
        if (swapped != null) original.alreadyUsed = false;
        return swapped;
    }

    private TextureDefinition FindOriginalInPool(TextureDefinition clone)
    {
        TextureDefinition[] pool = GetPool(clone.type);
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].id == clone.id) return pool[i];
        }
        Debug.LogWarning("Could not find Original from Clone.");
        return null;
    }

    public TextureDefinition[] GetPool(TextureType type)
    {
        TextureDefinition[] pool = type switch
        {
            TextureType.asr => ASR,
            TextureType.aob => AOB,
            TextureType.svn => SVN,
            TextureType.vsc => VSC,
            TextureType.obj => OBJ,
            _ => SVN
        };
        return pool;
    }
}
