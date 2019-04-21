using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathScript : MonoBehaviour
{
    public delegate void FinishSequence();
    public SpriteRenderer render;
    public Animator anim;
    public ParticleSystem particles;
    public float sequenceDuration;
    [HideInInspector]
    public FinishSequence onFinishSequence;

    public void StartSequence()
    {
        render.enabled = true;
        anim.enabled = true;
        particles.Play();
        Invoke("EndSequence", sequenceDuration);
    }

    public void EndSequence()
    {
        if (onFinishSequence != null)
        {
            onFinishSequence();
        }
    }
}
