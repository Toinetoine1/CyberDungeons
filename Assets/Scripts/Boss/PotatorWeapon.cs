using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;

public class PotatorWeapon : MonoBehaviour
{
    public GameObject Bullet;
    
    public float startAngle;
    public float endAngle;

    public float fireInterval;
    private float currInterval;

    private void Update()
    {
        if (currInterval > 0)
        {
            currInterval -= Time.deltaTime;
        }
    }

    private void Fire()
    {
        
    }

    private void fireABullet(Vector2 target)
    {
        GameObject bullet = PhotonNetwork.Instantiate(Bullet.name, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().setupWithVector(target);
    }
}
