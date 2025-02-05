using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float life = 3;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnCollisionEnter(Collision collision)
    {
      Destroy(collision.gameObject);
    }
}
