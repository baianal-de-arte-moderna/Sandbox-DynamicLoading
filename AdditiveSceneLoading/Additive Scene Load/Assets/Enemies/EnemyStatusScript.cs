using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStatusScript : MonoBehaviour
{
    public int hp;
    public GameObject[] DeathObjects;

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
    }

    public void Unload() 
    {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("PlayerBullet"))
        {
            hp--;
        }
    }
}
