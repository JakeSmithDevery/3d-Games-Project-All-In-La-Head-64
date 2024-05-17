using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int Damage = 10;
    public int Health = 100;
    public int MaxHealth = 100;

    public void AddHealth(int amount)
    {
        Health = Health + amount;

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    public void SubtractHealth(int amount)
    {
        Health = Health - amount;
        if (Health <= 0)
        {
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {

        Destroy(gameObject);
    }

    public virtual void HandleCollision(GameObject otherObject)
    {
        if (otherObject.CompareTag("Bullet"))
        {
            
        }

        if (otherObject.CompareTag("Melee"))
        {

        }
    }
    private void Update()
    {
        if (Health <= 0)
        { 
          OnDeath() ;
        }
    }
    public GameObject Bullet;
    public Transform BulletSpawner;

    private float bulletTime;
    private void Shoot()
    {
        GameObject bulletObj = Instantiate(Bullet, BulletSpawner.transform.position, BulletSpawner.transform.rotation) as GameObject;
    }
}
