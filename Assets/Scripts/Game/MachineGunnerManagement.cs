using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MachineGunnerManagement : EnnemyWeapon
{
    public int nbBullet;
    protected int currNbBullet;

    public bool isShooting;

    public float timeBetweenBullet;
    protected float currTimeBetweenBullet;


    // Start is called before the first frame update
    void Start()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        if (!pool.ResourceCache.ContainsKey(Bullet.name))
            pool.ResourceCache.Add(Bullet.name, Bullet);
        isShooting = false;
        _photonView = PhotonView.Get(this);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isShooting && currNbBullet == 0)
            currNbBullet = nbBullet;
        
        if (currInterval > 0)
        {
            currInterval -= Time.deltaTime;
        }

        if (currTimeBetweenBullet > 0)
            currTimeBetweenBullet -= Time.deltaTime;

        if (currNbBullet != 0 && isShooting && currTimeBetweenBullet <= 0 && currInterval <= 0)
        {
            if (_photonView.IsMine)
            {
                fireABullet();
                currTimeBetweenBullet = timeBetweenBullet;
                currNbBullet -= 1;
                isShooting = currNbBullet != 0;
                if (!isShooting)
                {
                    currInterval = firingInterval;
                }
            }
        }
    }
}

