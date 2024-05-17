using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
[RequireComponent(typeof(Rigidbody))]
public class BulletEnemy : MonoBehaviour
{
    public int BulletDamage = 5;
    private PlayerLocomotion playerHealth;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="Player")
        {
            if(playerHealth == null) 
            {
               playerHealth = collision.gameObject.GetComponent<PlayerLocomotion>();
            }
            playerHealth.TakeDamage(BulletDamage);
            Destroy(gameObject);
        }
    }
}

