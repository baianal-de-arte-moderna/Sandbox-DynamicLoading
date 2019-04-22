using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BossScript : MonoBehaviour
{
    //=====================
    // DELEGATES
    //=====================
    public delegate void BossHealthChanged(int newValue, int oldValue);
    public delegate void FinishDeath();
    public delegate void FinishFighting();
    public delegate void FinishPresentation();

    //=====================
    // AUXILIARY GAME OBJECTS AND COMPONENTS
    //=====================
    BossHitScript hit;
    public BossPresentationScript PresentationScript;
    public BossDeathScript DeathScript;

    //=====================
    // INTERNAL COMPONENTS
    //=====================
    Animator animator;
    public int damageTakenPerShot;
    bool invul;
    public int hp;

    //=====================
    // EVENTS
    //=====================
    public FinishPresentation onFinishPresentation;
    public FinishDeath onFinishDeath;
    public FinishFighting onFinishFighting;
    public BossHealthChanged onBossHealthChange;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        invul = true;
        hit = GetComponent<BossHitScript>();
    }

    public void StartPresentation()
    {
        animator.SetTrigger("Start");
        PresentationScript.onFinishSequence += StartFight;
        PresentationScript.StartSequence();
    }

    public void StartFight()
    {
        PresentationScript.onFinishSequence -= StartFight;
        if (onFinishPresentation != null)
        {
            onFinishPresentation();
        }
        SetVulnerable(true);
    }

    public void EndFight()
    {
        DeathScript.onFinishSequence -= EndFight;
        if (onFinishDeath != null)
        {
            onFinishDeath();
        }
    }

    public void SetVulnerable(bool vulnerable)
    {
        invul = !vulnerable;
    }

    public void Die()
    {
        if (onFinishFighting != null)
        {
            onFinishFighting();
        }
        DeathScript.onFinishSequence += EndFight;
        DeathScript.StartSequence();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!invul && other.collider.CompareTag("PlayerBullet"))
        {
            hp -= damageTakenPerShot;
            if (onBossHealthChange != null)
            {
                onBossHealthChange(hp, hp + damageTakenPerShot);
            }
            if (hp <= 0)
            {
                other.otherCollider.enabled = false;
                Die();
            }
            else if (hit != null)
            {
                hit.Hit();
            }
        }
    }
}
