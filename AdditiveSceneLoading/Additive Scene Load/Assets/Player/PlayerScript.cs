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
    public float shotDelay;
    float shotCooldown;
    public Transform Weapon;
    public GameObject Bullet;
    int grounded;
    public PlayerStatusScript status;
    void Start()
    {
        animator.SetBool("Running", false);
        alive = false;
        coyoteTimeCounter = coyoteTime;
        shotCooldown = shotDelay;
        grounded = 0;

        status.onHealthChange += HealthChange;

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
            grounded = feet.GetContacts(filter, points);
            animator.SetInteger("Grounded", grounded);            

            // Shooting
            bool shooting = Input.GetKey(KeyCode.K);
            animator.SetBool("Shooting", shooting);

            shotCooldown += Time.deltaTime;
            if (shooting && shotCooldown > shotDelay) {
                shotCooldown = 0f;
                Shoot(Bullet);
            }

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
        var position = rend.flipX? Weapon.position + Vector3.left * 1.5f:Weapon.position;
        if (grounded == 0)
            position += Vector3.up * 0.4f;
        var newBullet = Instantiate(
            bullet,
            position,
            Quaternion.identity
        );
        newBullet.GetComponent<BulletScript>().Shoot(rend.flipX? Vector2.left:Vector2.right, speed * 2);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death")) {
            Die();
        }
    }
    public void Die()
    {
        alive = false;
        rigid.gravityScale = 0;
        SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Additive);
    }

    void HealthChange(int newHp) 
    {
        // TODO: Death Animation
        if (newHp <= 0)
            Die();
    }
}
