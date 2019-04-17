using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDelayFIll : MonoBehaviour
{
    public RectTransform referenceFill;
    public float threshold;
    public float speed;
    Vector2 targetAnchorMax;
    Vector2 targetAnchorMin;
    bool triggered;
    RectTransform thisFill;
    float thresholdWait;

    void Start()
    {
        triggered = false;
        thisFill = GetComponent<RectTransform>();
        thresholdWait = 0f;

        targetAnchorMax = referenceFill.anchorMax;
        targetAnchorMin = referenceFill.anchorMin;

        thisFill.anchorMax = targetAnchorMax;
        thisFill.anchorMin = targetAnchorMin;
    }

    void FixedUpdate()
    {
        if (!triggered)
        {
            triggered = (targetAnchorMax != referenceFill.anchorMax || targetAnchorMin != referenceFill.anchorMin);
            if (triggered)
            {
                thresholdWait = 0f;
                targetAnchorMax = referenceFill.anchorMax;
                targetAnchorMin = referenceFill.anchorMin;
            }
        } 
        else
        {
            thresholdWait += Time.deltaTime;
            if (thresholdWait >= threshold)
            {
                thisFill.anchorMax = Vector2.Lerp(
                    thisFill.anchorMax,
                    targetAnchorMax,
                    speed
                );

                thisFill.anchorMin = Vector2.Lerp(
                    thisFill.anchorMin,
                    targetAnchorMin,
                    speed
                );

                if (Vector2.Distance(targetAnchorMax, thisFill.anchorMax) < 0.001f &&
                    Vector2.Distance(targetAnchorMin, thisFill.anchorMin) < 0.001f)
                {
                    triggered = false;
                }
            }
        }
    }

}
