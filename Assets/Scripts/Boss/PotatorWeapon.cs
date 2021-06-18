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

    public Transform target;
    private PotatorRoll _potatorRoll;

    public int amountOfBullet;
    
    public float startAngle;
    public float endAngle;

    public float fireInterval;
    private float currInterval;


    private void Start()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        if (!pool.ResourceCache.ContainsKey(Bullet.name))
            pool.ResourceCache.Add(Bullet.name, Bullet);
        _potatorRoll = GetComponent<PotatorRoll>();
    }

    void Update()
    {
        if (currInterval > 0)
        {
            currInterval -= Time.deltaTime;
        }
        
        if (currInterval <= 0 &&
            target != null && !Physics2D.Linecast(transform.position, target.position, 1 << LayerMask.NameToLayer("WallColider")) && _potatorRoll._state == PotatorRoll.State.Normal)
        {
            if (PhotonView.Get(this).IsMine)
            {
                Fire();
                currInterval = fireInterval;
            }
        }
    }

    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / amountOfBullet;
        float angle = startAngle;

        for (int i = 0; i < amountOfBullet + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
            
            Vector3 bulletVector = new Vector2(bulDirX,bulDirY);
            Vector2 bulletDir = (bulletVector - transform.position).normalized;

            fireABullet(bulletDir);
            
            angle -= angleStep;
        }
    }
    
    private void fireABullet(Vector2 target)
    {
        GameObject bullet = PhotonNetwork.Instantiate(Bullet.name, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().setupWithVector(target);
    }
}
