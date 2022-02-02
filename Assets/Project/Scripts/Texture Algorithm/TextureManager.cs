using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureManager : Singleton<TextureManager>
{
    public DistributionSetting ASR;
    public DistributionSetting AOB;
    public DistributionSetting SVN;
    public DistributionSetting VSC;
    public DistributionSetting OBJ;

    void Start()
    {
        DistributeTextures();
    }

    public void DistributeTextures()
    {
        TextureDifficulty currentLevel = SessionInstance.instance.session.textures;
        float currentDifficulty = SessionInstance.instance.session.difficulty;

        float mappedDifficulty = currentLevel switch
        {
            TextureDifficulty.easy => currentDifficulty.Remap(1, 5, 8, 12),
            TextureDifficulty.medium => currentDifficulty.Remap(1, 5, 12, 16.5f),
            TextureDifficulty.hard => currentDifficulty.Remap(1, 5, 12, 16.5f),
            _ => currentDifficulty.Remap(1, 5, 8, 16.5f)
        };

        Blueprint bp = new Blueprint(
            ASR.GetSetting(currentLevel),
            AOB.GetSetting(currentLevel),
            SVN.GetSetting(currentLevel),
            VSC.GetSetting(currentLevel),
            OBJ.GetSetting(currentLevel)
        );

        Debug.Log(bp.ToString());
        bp.DistributeTextures(mappedDifficulty, currentLevel);
        Debug.Log(bp.ToString());
        bp.ApproachOptimumDifficulty(mappedDifficulty);
        Debug.Log(bp.ToString());
    }
}

public enum TextureType {asr = 0, aob = 1, svn = 2, vsc = 3, obj = 4}