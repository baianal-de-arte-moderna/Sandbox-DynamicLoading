using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObjectScript : MonoBehaviour
{
    Vector2 direction;
    float force;
    Rigidbody2D rigid;
    public Collider2D triggerCollider;
    SpriteRenderer render;
    public ParticleSystem particles;
    float colorSpeed;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();

        transform.position += Vector3.up;

        direction = new Vector2(
            Random.value - 0.5f,
            1f
        ).normalized;
        force = rigid.mass * 1000f;

        colorSpeed = 0.0005f;
        
        rigid.AddForce(direction * force);
        rigid.AddTorque((Random.value - 0.5f) * 50f);
    }

    // Update is called once per frame
    void Update()
    {
        render.color = Color.Lerp(
            render.color,
            Color.clear,
            colorSpeed
        );
        if (render.color.a < 0.1f) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        triggerCollider.enabled = false;
        if (particles)
        {
            // Reset particles rotation so animation plays in the right direction
            particles.transform.rotation = Quaternion.identity;
            particles.Play();
        }
        colorSpeed = 0.05f;
        rigid.velocity = Vector2.zero;
    }
}
