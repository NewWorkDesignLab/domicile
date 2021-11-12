using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectScaler : MonoBehaviour
{
    public RectTransform can;
    public RectTransform tr;
    public float maxAspectRatio = .7f;

    // Start is called before the first frame update
    void Start()
    {
        float currentAspect = (float)can.rect.width / (float)can.rect.height;

        float targetWidth = can.rect.width;
        if (currentAspect > maxAspectRatio)
        {
            targetWidth = can.rect.height * maxAspectRatio;
        }

        tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
