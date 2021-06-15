using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

public class EnnemyWeapon : MonoBehaviour
{
    [SerializeField] public GameObject Bullet;

    protected float firingInterval;
    protected float currInterval;
    
    public Transform target;


    private void Start()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        if (!pool.ResourceCache.ContainsKey(Bullet.name))
            pool.ResourceCache.Add(Bullet.name, Bullet);
    }

    private void Update()
    {
        if (currInterval > 0)
        {
            currInterval -= Time.deltaTime;
        }
    }

    public void fire()
    {
        if (currInterval <= 0)
        {
            fireABullet();
            currInterval = firingInterval;
        }
    }

    protected void fireABullet()
    {
        GameObject newBullet = PhotonNetwork.Instantiate(Bullet.name, transform.position, Quaternion.identity);
        if (this is SniperManagement)
            newBullet.GetComponent<Bullet>().EnemiSniperSetup(target);
        else
            newBullet.GetComponent<Bullet>().EnemiSetup(target);
    }

    ///TODO script pour le sniper
    /// TODO script pour les bosses
}
