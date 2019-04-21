using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BossHitScript : MonoBehaviour
{
    SpriteRenderer render;
    bool hitted;
    float hitDuration;
    float currentHitDuration;
    float hitTiming;
    float hitSpeed;
    Color[] hitColors;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();

        hitted = false;
        hitDuration = 1.5f;
        hitTiming = 0.3f;
        hitSpeed = 0.1f;
        currentHitDuration = 0f;
        hitColors = new Color[] {
            Color.clear,
            Color.white
        };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hitted)
        {
            render.color = Color.Lerp(
                render.color,
                hitColors[Mathf.FloorToInt(currentHitDuration / hitTiming) % hitColors.Length],
                hitSpeed
            );
            hitted = hitDuration > currentHitDuration;
            currentHitDuration += Time.deltaTime;
        }
        else
        {
            render.color = Color.white;
        }
    }
    public void Hit() 
    {
        hitted = true;
        currentHitDuration = 0f;
    }
}
