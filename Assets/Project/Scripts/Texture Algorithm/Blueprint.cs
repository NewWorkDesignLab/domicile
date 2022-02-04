using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Blueprint
{
    private int asr;
    private int aob;
    private int svn;
    private int vsc;
    private int obj;

    public TextureDefinition[] selectedTextures;
    public TextureVariant[] selectedPlacements;

    public Blueprint(int asr_slots, int aob_slots, int svn_slots, int vsc_slots, int obj_slots) 
    {
        asr = asr_slots;
        aob = aob_slots;
        svn = svn_slots;
        vsc = vsc_slots;
        obj = obj_slots;

        int sum = asr_slots + aob_slots + svn_slots + vsc_slots + obj_slots;
        selectedTextures = new TextureDefinition[sum];
        selectedPlacements = new TextureVariant[sum];
    }

    public void DistributeTextures(float targetDifficulty, TextureDifficulty textureDifficulty)
    {
        int[] slotCounts = new int[5] {asr, aob, svn, vsc, obj};
        int currentType = 0;
        int breakpoint = slotCounts[currentType];

        for (int i = 0; i < selectedTextures.Length; i++)
        {
            // set new breakpoint depending on number of slots for each type
            // needs to be in a loop in case slotCounts[currentType] is 0
            int max = 20;
            while (i == breakpoint && max > 0)
            {
                max--;
                currentType++;
                breakpoint += slotCounts[currentType];
            }

            // select texture
            selectedTextures[i] = TexturePool.instance.GetTexture((TextureType)currentType, textureDifficulty);
        }
    }

    public void ApproachOptimumDifficulty(float targetDifficulty)
    {
        float differenceInDifficultyStart = CurrentDifficulty() - targetDifficulty;
        Debug.Log($"Going to Approach optimum Difficulty... Startet with Difference {differenceInDifficultyStart} Total {CurrentDifficulty()}");

        int maxIterations = 50;
        while (CurrentDifficulty() != targetDifficulty && maxIterations >= 0)
        {
            maxIterations--;
            int randomTextureIndex = Random.Range(0, selectedTextures.Length);
            TextureDefinition current = selectedTextures[randomTextureIndex];

            float currentDifference = CurrentDifficulty() - targetDifficulty;
            TextureDefinition swappedTexture = TexturePool.instance.TrySwapTexture(current, currentDifference);

            if (swappedTexture != null)
            {
                selectedTextures[randomTextureIndex] = swappedTexture;
                float resultDifference = CurrentDifficulty() - targetDifficulty;
                Debug.Log($"Result Iteration {20 - maxIterations}: Difference {resultDifference} total {CurrentDifficulty()}");
            }
        }
        Debug.Log("Finnished Approach");
    }

    public void SelectPlacementsForTextures()
    {
        for (int i = 0; i < selectedTextures.Length; i++)
        {
            selectedPlacements[i] = selectedTextures[i].GetRandomTextureVariant();
        }
    }

    public float CurrentDifficulty()
    {
        float? sum = selectedTextures?.Sum(x => x?.GetFloatDiffuculty());
        return sum == null ? 0f : (float)sum;
    }

    public override string ToString()
    {
        string retStrn = System.String.Format("Schadenverteilung enthält {0} Texturen mit einer Diff von {1}:", selectedTextures.Length, CurrentDifficulty());
        for (int i = 0; i < selectedTextures.Length; i++)
        {
            retStrn += System.String.Format("\nIndex {0}: {1} on Position {2}", i, selectedTextures[i]?.ToString(), selectedPlacements[i]?.placementName);
        }
        return retStrn;
    }
}