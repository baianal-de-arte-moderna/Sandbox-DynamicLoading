﻿using System.Collections;
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
    public ParticleSystem deathParticles;
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
        status.onScrapChange += ScrapChange;

        points = new Collider2D[10];
        // Only Down Collisions
        filter.SetNormalAngle(90f, 90f);
        filter.SetLayerMask(LayerMask.GetMask("Default"));
        
        Invoke("Go", startDelay);
        Invoke("Spawn", spawnDelay);
    }
    void FixedUpdate()
    {
        grounded = feet.GetContacts(filter, points);
        animator.SetInteger("Grounded", grounded); 
        if (alive)
        {
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
            status.onHealthChange(0, 1);
        }
    }
    public void Die()
    {
        rend.enabled = false;
        deathParticles.Play();
        alive = false;
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 0;
        Invoke("GameOver", 1.5f);
    }

    public void GameOver()
    {
        alive = false;
        SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Additive);
    }

    public void EndHittedAnimation() 
    {
        animator.SetBool("Hitted", false);
        alive = true;
        status.invul = false;
    }

    void HealthChange(int newHp, int oldHp)
    {
        if (newHp <= 0)
            Die();
        else if (oldHp > newHp) {
            alive = false;
            status.invul = true;
            if (rend.flipX)
                rigid.velocity = new Vector2(speed, speed);
            else
                rigid.velocity = new Vector2(-speed, speed);
            animator.SetBool("Hitted", true);
            //animator.SetBool("Running", false);
            //animator.SetBool("Shooting", false);
        }
    }

    void ScrapChange(int newScrap, int oldScrap)
    {
        if (newScrap >= 100)
        {
            SceneManagerScript.GM.FinalizePlatformSpawning();
        }
            
    }
}
