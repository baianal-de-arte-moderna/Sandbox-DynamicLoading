using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusScript : MonoBehaviour
{
    public delegate void HealthChanged(int newValue, int oldValue);
    public delegate void ScrapChanged(int newValue, int oldValue);

    [Range(0, 100)]
    public int hp;
    
    [Range(0, 100)]
    public int scrapTotal;
    public HealthChanged onHealthChange;
    public ScrapChanged onScrapChange;
    public bool invul;
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!invul)
        {
            var oldHp = hp;
            if (other.collider.CompareTag("EnemyBullet"))
            {
                var bulletPower = other.collider.GetComponent<BulletData>().power;
                hp -= bulletPower;
                if (onHealthChange != null) 
                {
                    onHealthChange(hp, oldHp);
                }
            } else if (other.collider.CompareTag("Enemy"))
            {
                hp -= 10;
                if (onHealthChange != null) 
                {
                    onHealthChange(hp, oldHp);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Scrap"))
        {
            var oldScrap = scrapTotal;
            var scrapAmount = other.GetComponent<ScrapData>().value;
            scrapTotal += scrapAmount;
            if (onScrapChange != null)
            {
                onScrapChange(scrapTotal, oldScrap);
            }
        }
    }
}
