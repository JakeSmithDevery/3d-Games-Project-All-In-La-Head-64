using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class enemyslow : enemy
{
    public GameObject Bullet;
    public Transform BulletSpawner;

    private float bulletTime;
    private void Shoot()
    {
        GameObject bulletObj = Instantiate(Bullet, BulletSpawner.transform.position, BulletSpawner.transform.rotation) as GameObject;
    }
}
