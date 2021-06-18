using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using Photon.Pun; 

public class RocketeerWeapon : MonoBehaviour
{
    public float startAngle;
    public float endAngle;

    public int amountOfBullet;

    public GameObject Bullet;

    private void Start()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        if (!pool.ResourceCache.ContainsKey(Bullet.name))
            pool.ResourceCache.Add(Bullet.name, Bullet);
    }

    public void Fire()
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

            if (amountOfBullet == 25)
                amountOfBullet = 20;
            else
                amountOfBullet = 25;
        }
    }
    
    private void fireABullet(Vector2 target)
    {
        GameObject bullet = PhotonNetwork.Instantiate(Bullet.name, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().setupWithVector(target);
    }
}
