using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStatusScript : MonoBehaviour
{
    public int hp;
    public GameObject[] DeathObjects;
    Transform trans;
    Camera mainCamera;

    void Start()
    {
        trans = transform;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) 
        {
            if (DeathObjects.Length > 0) 
            {
                var pieceCount = Random.Range(2, 5);
                for (var i = 0; i < pieceCount; i++)
                {
                    Instantiate(
                        DeathObjects[Random.Range(0, DeathObjects.Length)],
                        transform.position,
                        Quaternion.identity
                    );
                }
            }
            gameObject.SetActive(false);
            Unload();
        }
        if (trans.position.x - mainCamera.transform.position.x > 60f) {
            Destroy(gameObject);
        }
    }

    public void Unload() 
    {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("PlayerBullet"))
        {
            var bulletPower = other.collider.GetComponent<BulletData>().power;
            hp -= bulletPower;
        }
    }
}
