using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MetScript : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigid;
    Transform trans;
    Collider2D[] points;
    bool walk;
    bool roll;
    void Start()
    {
        points = new Collider2D[10];
        walk = false;
        roll = false;
        trans = transform;
        RandomizeNextAction();
    }
    void FixedUpdate()
    {
        var grounded = rigid.GetContacts(points);
        if (grounded > 0)
        {
            animator.SetFloat("RandomIdleTime", Random.value);
            if (walk) 
            {
                rigid.velocity = new Vector2(-1.5f, rigid.velocity.y);
            }
            else if (roll) 
            {
                rigid.velocity = new Vector2(-4f, rigid.velocity.y);
            }
        }
        // Die on Fall
        if (-10f > trans.position.y) {
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }

    public void Walk() 
    {
        walk = true;
    }

    public void Stop()
    {
        walk = false;
        roll = false;
        rigid.velocity = Vector2.zero;
    }

    public void Roll() 
    {
        roll = true;
    }

    public void Shoot() 
    {
        Debug.Log("Met Shot");
    }

    public void RandomizeNextAction()
    {
        animator.SetInteger("Action", Random.Range(0, 3));
    }
}
