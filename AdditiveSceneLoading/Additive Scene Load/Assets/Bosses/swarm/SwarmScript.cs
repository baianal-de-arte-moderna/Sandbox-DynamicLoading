using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossScript))]
public class SwarmScript : MonoBehaviour
{
    Animator animator;
    SpriteRenderer render;
    public Animator aimAnimator;
    Rigidbody2D rigid;
    BossScript boss;
    float distance;
    bool fighting;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        boss = GetComponent<BossScript>();
        render = GetComponent<SpriteRenderer>();

        boss.onFinishPresentation += StartFight;
        boss.onFinishFighting += EndFight;
        distance = Camera.main.orthographicSize * 2.5f;
        fighting = false;
    }

    void OnBecameInvisible()
    {
        if (fighting)
        {
            rigid.velocity = Vector2.zero;
            SetNewSwarmDirection(Random.insideUnitCircle);
            SetAttackTarget(transform.parent.position);
            Invoke("Attack", Random.Range(1f, 2f));
        }
    }
    public void StartFight()
    {
        fighting = true;
        Attack();
    }

    public void EndFight()
    {
        fighting = false;
        rigid.velocity = Vector2.zero;
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        render.enabled = false;
    }

    public void Attack()
    {
        aimAnimator.enabled = true;
        Invoke("Fly", 1.5f);
    }

    public void SetNewSwarmDirection(Vector2 direction)
    {
        transform.localPosition = -distance * direction.normalized;
        render.flipY = direction.x < 0f;
    }

    public void SetAttackTarget(Vector3 position)
    {
        transform.right = position - transform.position;
    }

    public void Fly()
    {
        animator.enabled = false;
        aimAnimator.enabled = false;
        rigid.AddForce(transform.right * rigid.mass * 700f);
    }
}
