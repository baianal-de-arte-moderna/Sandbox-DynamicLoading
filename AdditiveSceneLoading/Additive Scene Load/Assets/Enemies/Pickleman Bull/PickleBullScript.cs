using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickleBullScript : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigid;
    Transform trans;
    Collider2D[] points;
    bool walk;
    void Start()
    {
        points = new Collider2D[10];
        walk = false;
        trans = transform;
    }
    void FixedUpdate()
    {
        var grounded = rigid.GetContacts(points);
        if (grounded > 0)
        {
            if (walk) 
            {
                rigid.velocity = new Vector2(-2.5f, rigid.velocity.y);
            }
        }
        // Die on Fall
        if (-10f > trans.position.y) {
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
    void OnBecameVisible()
    {
        Walk();
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Walk() 
    {
        walk = true;
    }

    public void Stop()
    {
        walk = false;
        rigid.velocity = Vector2.zero;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("PlayerBullet"))
        {
            animator.SetTrigger("Hitted");
            walk = false;
            rigid.velocity = Vector2.zero;
        }
    }
}
