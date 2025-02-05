using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunAttack : MonoBehaviour
{

    public double maxAmmo = 10;
    public float ammoRegenerationDelay = 2f;
    public float ammoRegenerationRate = 1f;
    public int strongAttackDamage = 30;
    public float strongAttackRange = 10f;
    public LayerMask attackLayer;

    public double currentAmmo = 0;
    public bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        StartCoroutine(RegenerateAmmo());
    }

    // Update is called once per frame
    public void CheckAmmo()
    {
        
            if (currentAmmo > 0)
            {
                isAttacking = true;
                StartCoroutine(PerformStrongAttack());
            }
            else
            {
                Debug.Log("Out of ammo!");
                // Play out of ammo sound or visual feedback
            }
        
    }

    public IEnumerator PerformStrongAttack()
    {
        currentAmmo--;

        RaycastHit hit;
        Debug.DrawRay(transform.position, -transform.forward * strongAttackRange, Color.red, 1f);
        if (Physics.Raycast(transform.position, transform.forward, out hit, strongAttackRange, attackLayer))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                // Get the enemy's health component
                enemy enemyHealth = hit.collider.GetComponent<enemy>();
                if (enemyHealth != null)
                {
                    // Log damage application
                    Debug.Log($"Damaging enemy: {hit.collider.name}");
                    // Subtract health from the enemy
                    enemyHealth.SubtractHealth(strongAttackDamage);
                }
            }
        }

        // Add gun recoil animation or other effects here

        yield return new WaitForSeconds(2f); // Attack cooldown

        isAttacking = false;
    }

    public IEnumerator RegenerateAmmo()
    {
        while (true)
        {
            yield return new WaitForSeconds(ammoRegenerationDelay);
            if (currentAmmo < maxAmmo)
            {
                currentAmmo++;
                // Update UI or display ammo regeneration message
            }
        }
    }
}
