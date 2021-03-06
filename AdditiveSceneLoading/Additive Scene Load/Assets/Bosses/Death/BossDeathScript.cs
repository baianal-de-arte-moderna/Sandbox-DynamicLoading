﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossDeathScript : MonoBehaviour
{
    public delegate void FinishSequence();
    public SpriteRenderer[] render;
    public Animator[] anim;
    public ParticleSystem[] particles;
    public float sequenceDuration;
    public float sequenceDelay;
    [HideInInspector]
    public FinishSequence onFinishSequence;

    public void StartSequence()
    {
        Invoke("DelayedStart", sequenceDelay);
        Invoke("EndSequence", sequenceDuration);
    }

    void DelayedStart() 
    {
        foreach (var x in render)
            x.enabled = true;
        foreach (var x in anim)
            x.enabled = true;
        foreach (var x in particles)
            x.Play();
    }

    public void EndSequence()
    {
        if (onFinishSequence != null)
        {
            onFinishSequence();
        }
    }
}
