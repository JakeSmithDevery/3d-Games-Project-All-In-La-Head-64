using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class enemy : MonoBehaviour
{
    public int Damage = 10;
    public int Health = 100;
    public int MaxHealth = 100;
    private PlayerLocomotion playerHealth;

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
            // Handle enemy death
            Destroy(gameObject);
        }
    }

    public virtual void OnDeath()
    {

        Destroy(gameObject);
    }

    private void Update()
    {
        if (Health <= 0)
        {
            OnDeath();
        }
        Shoot();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerHealth == null)
            {
                playerHealth = collision.gameObject.GetComponent<PlayerLocomotion>();
            }
            playerHealth.TakeDamage(Damage);
        }
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
