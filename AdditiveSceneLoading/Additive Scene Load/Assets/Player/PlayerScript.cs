using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public Animator animator;
    public Rigidbody2D rigid;
    public SpriteRenderer rend;
    public bool alive;
    ContactPoint2D[] points;
    public int coyoteTime;
    int coyoteTimeCounter;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        animator.SetBool("Running", true);
        alive = true;
        points = new ContactPoint2D[10];
        coyoteTimeCounter = coyoteTime;
    }
    void FixedUpdate()
    {
        // Control
        //if (Input.GetKey(KeyCode.D)) 
        //{
        //    rigid.velocity = new Vector2(speed, rigid.velocity.y);
        //    animator.SetBool("Running", true);
        //    rend.flipX = false;
        //} 
        //else if (Input.GetKey(KeyCode.A)) 
        //{
        //    rigid.velocity = new Vector2(-speed, rigid.velocity.y);
        //    animator.SetBool("Running", true);
        //    rend.flipX = true;
        //} 
        //else 
        //{
        //    animator.SetBool("Running", false);
        //}
        if (alive)
        {
            var isJumping = animator.GetCurrentAnimatorStateInfo(0).IsName("Jump");
            if (Input.GetKey(KeyCode.Space) && !isJumping)
            {
                rigid.velocity = new Vector2(speed, speed * rigid.gravityScale / 2);
                animator.SetTrigger("Jumping");
            } 
            else if (!Input.GetKey(KeyCode.Space) && isJumping && rigid.velocity.y > 0)
            {
                rigid.velocity = new Vector2(speed, 0f);
            }
            else
            {
                rigid.velocity = new Vector2(speed, rigid.velocity.y);
            }
            if (animator.GetInteger("Grounded") == 0 && !isJumping) {
                coyoteTimeCounter--;
                if (coyoteTimeCounter <= 0) {
                    animator.SetTrigger("Jumping");
                }
            } 
            else
            {
                coyoteTimeCounter = coyoteTime;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        other.GetContacts(points);
        if (points[0].normal.y > 0)
            animator.SetInteger("Grounded", animator.GetInteger("Grounded") + 1);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        animator.SetInteger("Grounded", Mathf.Max(animator.GetInteger("Grounded") - 1, 0));
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death")) {
            alive = false;
            rigid.gravityScale = 0;
            SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Additive);
        }
    }
}
