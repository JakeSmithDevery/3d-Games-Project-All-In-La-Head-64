using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int Damage = 10;
    public int Health = 100;
    public int MaxHealth = 100;

    public int expAmount = 20;

    public PlayerStats PlayerStats;

    private void Start()
    {
        PlayerStats = FindAnyObjectByType<PlayerStats>();
    }

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
        Health -= amount;
        if (Health <= 0)
        {
            PlayerStats.HandleExpChange(expAmount);
            // Handle enemy death
            Destroy(gameObject);
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
        Shoot();
    }
    public GameObject Bullet;
    public Transform BulletSpawner;
    public float speed;

    private float bulletTime;
    [SerializeField] private float timer =5;
    private void Shoot()
    {
        bulletTime = Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = timer;

        GameObject bulletObj = Instantiate(Bullet, BulletSpawner.transform.position, BulletSpawner.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * speed);
        Destroy(bulletObj, 0.1f);
    }
}
