using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody2D rigid;
    Transform trans;
    Camera mainCamera;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        trans = transform;
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (trans.position.x - mainCamera.transform.position.x > 10f) {
            Destroy(gameObject);
        }
    }

    public void Shoot(Vector2 direction, float speed) 
    {
        rigid.velocity = direction * speed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
