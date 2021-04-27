using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

public class EnnemyWeapon : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;

    public float firingInterval;
    private float currInterval;

    private void Start()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        pool.ResourceCache.Add(Bullet.name, Bullet);
    }

    private void Update()
    {
        if (currInterval > 0)
        {
            currInterval -= Time.deltaTime;
        }
    }

    public void fire(Transform Target)
    {
        if (currInterval <= 0)
        {
            GameObject newBullet = PhotonNetwork.Instantiate(Bullet.name, transform.position, Quaternion.identity);
            newBullet.GetComponent<Bullet>().EnemiSetup(Target);
            currInterval = firingInterval;
        }
    }

    
}
