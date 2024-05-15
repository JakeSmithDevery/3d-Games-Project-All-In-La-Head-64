using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public int rayCount = 5; // Number of rays to shoot
    public float angle = 45f; // Angle in degrees
    public float meleeRange = 2f;
    public LayerMask attackLayer;
    public float damage = 10f;

    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void PerformMeleeAttack()
    {
        for (int i = 0; i < rayCount; i++)
        {
            float angleOffset = (i - (rayCount - 1) / 2f) * angle / (rayCount - 1);
            Vector3 direction = Quaternion.Euler(0, angleOffset, 0) * transform.forward;

            Debug.DrawRay(transform.position, direction * meleeRange, Color.red, 1f); // Draw debug ray

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, meleeRange, attackLayer))
            {
                
                if (hit.collider != null)
                {
                    Enemy health = hit.collider.GetComponent<Enemy>();
                    if (health != null)
                    {
                        health.TakeDamage(damage);
                    }
                }
            }
        }


    }
}
