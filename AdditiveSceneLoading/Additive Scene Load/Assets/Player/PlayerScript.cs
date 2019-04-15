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
    public Collider2D feet;
    public bool alive;
    Collider2D[] points;
    ContactFilter2D filter;
    public int coyoteTime;
    int coyoteTimeCounter;
    public float startDelay;
    public float spawnDelay;
    public Transform Weapon;
    void Start()
    {
        animator.SetBool("Running", false);
        alive = false;
        coyoteTimeCounter = coyoteTime;

        points = new Collider2D[10];
        // Only Down Collisions
        filter.SetNormalAngle(90f, 90f);
        
        Invoke("Go", startDelay);
        Invoke("Spawn", spawnDelay);
    }
    void FixedUpdate()
    {
        if (alive)
        {
            var grounded = feet.GetContacts(filter, points);
            animator.SetInteger("Grounded", grounded);            

            // Shooting
            animator.SetBool("Shooting", Input.GetKey(KeyCode.K));

            if (Input.GetKey(KeyCode.Space) && grounded > 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, speed * rigid.gravityScale / 2);
                animator.SetTrigger("Jumping");
            } 
            else if (!Input.GetKey(KeyCode.Space) && grounded == 0 && rigid.velocity.y > 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0f);
            }
            else
            {
                // Auto-Run
                // rigid.velocity = new Vector2(speed, rigid.velocity.y);
                // Controls
                if (Input.GetKey(KeyCode.D)) 
                {
                    rigid.velocity = new Vector2(speed, rigid.velocity.y);
                    animator.SetBool("Running", true);
                    rend.flipX = false;
                } 
                else if (Input.GetKey(KeyCode.A)) 
                {
                    rigid.velocity = new Vector2(-speed, rigid.velocity.y);
                    animator.SetBool("Running", true);
                    rend.flipX = true;
                } 
                else 
                {
                    rigid.velocity = new Vector2(
                        Mathf.Lerp(rigid.velocity.x, 0f, 0.1f),
                        rigid.velocity.y);
                    animator.SetBool("Running", false);
                }
            }
            if (grounded == 0) {
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

    void Go() {
        alive = true;
    }

    void Spawn() {
        rend.enabled = true;
    }

    public void Shoot(GameObject bullet) 
    {
        var newBullet = Instantiate(
            bullet,
            rend.flipX? Weapon.position + Vector3.left * 2:Weapon.position,
            Quaternion.identity
        );
        newBullet.GetComponent<BulletScript>().Shoot(rend.flipX? Vector2.left:Vector2.right, speed * 2);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death")) {
            alive = false;
            rigid.gravityScale = 0;
            SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Additive);
        }
    }
}
