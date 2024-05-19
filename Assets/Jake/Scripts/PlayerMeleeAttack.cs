using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public int rayCount = 5; // Number of rays to shoot
    public float angle = 45f; // Angle in degrees
    public float meleeRange = 2f;
    public LayerMask attackLayer;
    public int damage = 10;

    public bool isAttacking = false;

    public LayerMask AttackLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void PerformMeleeAttack()
    {
        Debug.Log("Performing Melee Attack");

        for (int i = 0; i < rayCount; i++)
        {
            // Calculate the angle offset for each ray
            float angleOffset = (i - (rayCount - 1) / 2f) * (angle / (rayCount - 1));
            Vector3 direction = Quaternion.Euler(0, angleOffset, 0) * -transform.forward;

            // Draw debug ray for visualization
            Debug.DrawRay(transform.position, direction * meleeRange, Color.red, 1f);

            // Perform the raycast
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, meleeRange, attackLayer))
            {
                // Log the name of the object hit
                Debug.Log($"Ray {i} hit object: {hit.collider.name}");

                // Check if the hit object is an enemy
                if (hit.collider.CompareTag("Enemy"))
                {
                    // Get the enemy's health component
                    enemy enemyHealth = hit.collider.GetComponent<enemy>();
                    if (enemyHealth != null)
                    {
                        // Log damage application
                        Debug.Log($"Damaging enemy: {hit.collider.name}");
                        // Subtract health from the enemy
                        enemyHealth.SubtractHealth(damage);
                    }
                    else
                    {
                        Debug.Log("Enemy component not found on hit object.");
                    }
                }
                else
                {
                    Debug.Log($"Ray {i} hit object: {hit.collider.name}, but it's not tagged as Enemy");
                }
            }
            else
            {
                Debug.Log($"Ray {i} did not hit any object.");
            }
        }
    }
}

