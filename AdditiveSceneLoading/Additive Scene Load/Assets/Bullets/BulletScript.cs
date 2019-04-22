using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody2D rigid;
    Transform trans;
    Camera mainCamera;
    bool isPlayerShot;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        trans = transform;
        mainCamera = Camera.main;
        isPlayerShot = CompareTag("PlayerBullet");
    }

    void Update()
    {
        if (trans.position.x - mainCamera.transform.position.x > 10f)
        {
            Destroy(gameObject);
            if (isPlayerShot) SceneManagerScript.GM.hittedShots += 1;
        }
    }

    public void Shoot(Vector2 direction, float speed) 
    {
        rigid.velocity = direction * speed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
        if (isPlayerShot) SceneManagerScript.GM.missedShots += 1;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        if (isPlayerShot) SceneManagerScript.GM.hittedShots += 1;
    }
}
