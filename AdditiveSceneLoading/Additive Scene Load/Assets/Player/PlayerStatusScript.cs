using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusScript : MonoBehaviour
{
    public delegate void HealthChanged(int newValue);
    public int hp;
    public HealthChanged onHealthChange;
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("EnemyBullet"))
        {
            var bulletPower = other.collider.GetComponent<BulletData>().power;
            hp -= bulletPower;
            if (onHealthChange != null) 
            {
                onHealthChange(hp);
            }
        } else if (other.collider.CompareTag("Enemy"))
        {
            hp -= 10;
            if (onHealthChange != null) 
            {
                onHealthChange(hp);
            }
        }
    }
}
