using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

public class EnnemyWeapon : MonoBehaviour
{
    [SerializeField] public GameObject Bullet;

    public float firingInterval;
    public float currInterval;

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

    public void fire(Transform Target)
    {
        if (currInterval <= 0)
        {
            fireABullet(Target);
            currInterval = firingInterval;
        }
    }

    public void fireABullet(Transform Target)
    {
        GameObject newBullet = PhotonNetwork.Instantiate(Bullet.name, transform.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().EnemiSetup(Target);
    }

    ///TODO script pour le sniper
    /// TODO script pour les bosses
}
