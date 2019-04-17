using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HammerJoeScript : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigid;
    public GameObject Bullet;
    public Transform Weapon;
    Transform trans;
    Collider2D[] points;
    void Start()
    {
        points = new Collider2D[10];
        trans = transform;
    }
    void FixedUpdate()
    {
        // Die on Fall
        if (-10f > trans.position.y) {
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
    public void Shoot() 
    {
        if (animator.GetFloat("ShotChance") > 0f)
        {
            animator.SetFloat("RestartChance", 0f);
            animator.SetFloat("ShotChance", 0f);
            var newBullet = Instantiate(
                Bullet,
                Weapon.position,
                Quaternion.identity
            );
            newBullet.GetComponent<BulletScript>().Shoot(Vector2.left, 10f);
        }
    }

    public void RandomizeShotTime()
    {
        animator.SetFloat("ShotChance", Random.value);
    }

    public void RandomizeRestartTime()
    {
        animator.SetFloat("RestartChance", Random.value);
    }
}
