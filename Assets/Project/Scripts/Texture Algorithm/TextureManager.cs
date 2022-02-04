using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TextureManager : NetworkBehaviour
{
    public DistributionSetting ASR;
    public DistributionSetting AOB;
    public DistributionSetting SVN;
    public DistributionSetting VSC;
    public DistributionSetting OBJ;

    public Blueprint blueprint;

    /// <summary>
    /// Called on every NetworkBehaviour when it is activated on a client.
    /// <para>Objects on the host have this function called, as there is a local client on the host. The values of SyncVars on object are guaranteed to be initialized correctly with the latest state from the server when this function is called on the client.</para>
    /// </summary>
    public override void OnStartClient()
    {
        if (SessionInstance.instance.session.target == SessionTarget.create)
        {
            DistributeTextures();
            EnableSelectedPlacements();
        }
    }

    private void DistributeTextures()
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

        blueprint = new Blueprint(
            ASR.GetSetting(currentLevel),
            AOB.GetSetting(currentLevel),
            SVN.GetSetting(currentLevel),
            VSC.GetSetting(currentLevel),
            OBJ.GetSetting(currentLevel)
        );

        blueprint.DistributeTextures(mappedDifficulty, currentLevel);
        blueprint.ApproachOptimumDifficulty(mappedDifficulty);
        blueprint.SelectPlacementsForTextures();
        Debug.Log(blueprint.ToString());
    }

    private void EnableSelectedPlacements()
    {
        for (int i = 0; i < blueprint.selectedPlacements.Length; i++)
        {
            if (blueprint.selectedPlacements[i] != null)
                blueprint.selectedPlacements[i].CmdSetState(true);
        }
    }
}

public enum TextureType {asr = 0, aob = 1, svn = 2, vsc = 3, obj = 4}