using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObjectScript : MonoBehaviour
{
    Vector2 direction;
    float force;
    Rigidbody2D rigid;
    SpriteRenderer render;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();

        direction = new Vector2(
            Random.value - 0.5f,
            1f
        ).normalized;
        force = Random.value * 5f + 10000f;
        
        rigid.AddForce(direction * force);
        rigid.AddTorque((Random.value - 0.5f) * 1000f);
    }

    // Update is called once per frame
    void Update()
    {
        render.color = Color.Lerp(
            render.color,
            Color.clear,
            0.015f
        );
        if (render.color.a < 0.1f) {
            Destroy(gameObject);
        }
    }
}
