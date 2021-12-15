using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DisableOnTextureHelper : MonoBehaviour
{
    public TextureDifficulty onlyShowOnTextureSetting;

    void Start()
    {
        if (NetworkServer.active || NetworkClient.active)
            StartCoroutine(CheckTexture());
        else
            gameObject.SetActive(false);
    }
    
    private IEnumerator CheckTexture()
    {
        while (OnlinePlayer.scenario == null)
            yield return null;

        bool show = OnlinePlayer.scenario.textures == onlyShowOnTextureSetting;
        gameObject.SetActive(show);
    }
}
