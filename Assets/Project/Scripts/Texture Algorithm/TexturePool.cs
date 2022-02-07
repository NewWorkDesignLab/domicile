using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TexturePool : Singleton<TexturePool>
{
    private TextureDefinition[] allTextures;
    private List<TextureDefinition> ASR = new List<TextureDefinition>();
    private List<TextureDefinition> AOB = new List<TextureDefinition>();
    private List<TextureDefinition> SVN = new List<TextureDefinition>();
    private List<TextureDefinition> VSC = new List<TextureDefinition>();
    private List<TextureDefinition> OBJ = new List<TextureDefinition>();

    void Start()
    {
        allTextures = GetComponentsInChildren<TextureDefinition> ();
        for (int i = 0; i < allTextures.Length; i++)
        {
            switch (allTextures[i].type)
            {
                case TextureType.asr:
                    ASR.Add(allTextures[i]);
                    break;
                case TextureType.aob:
                    AOB.Add(allTextures[i]);
                    break;
                case TextureType.svn:
                    SVN.Add(allTextures[i]);
                    break;
                case TextureType.vsc:
                    VSC.Add(allTextures[i]);
                    break;
                case TextureType.obj:
                    OBJ.Add(allTextures[i]);
                    break;
            }
        }
    }

    public TextureDefinition GetTexture(TextureType type, TextureDifficulty difficulty, bool checkDifficulty = true, bool allowFallbackWithoutDifficulty = true)
    {
        List<TextureDefinition> pool = GetPool(type);

        // shuffle to get random Texture
        pool.Shuffle();

        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].alreadyUsed) continue;

            if (!checkDifficulty || pool[i].difficulty == difficulty)
            {
                pool[i].alreadyUsed = true;
                return pool[i];
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

    public TextureDefinition TrySwapTexture(TextureDefinition current, float difference)
    {
        // return if already best choice in difficulty
        if (difference > 0 && current.difficulty == TextureDifficulty.easy) return null;
        if (difference < 0 && current.difficulty == TextureDifficulty.hard) return null;

        // find new texture with adjusted difficulty
        TextureDifficulty targetLevel = difference > 0 ? (TextureDifficulty)((int)current.difficulty - 1) : (TextureDifficulty)((int)current.difficulty + 1);
        TextureDefinition swapped = GetTexture(current.type, targetLevel, true, false);
        if (swapped != null) current.alreadyUsed = false;
        return swapped;
    }

    public List<TextureDefinition> GetPool(TextureType type)
    {
        List<TextureDefinition> pool = type switch
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
