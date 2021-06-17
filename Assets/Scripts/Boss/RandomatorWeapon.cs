using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using Photon.Pun;

public class RandomatorWeapon : MachineGunnerManagement
{
    private int Damage;
    private float Speed;

    private float switchTime;

    private bool typeOfFire; //true -> machinegunner | false -> normal

    void Start()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        if (!pool.ResourceCache.ContainsKey(Bullet.name))
            pool.ResourceCache.Add(Bullet.name, Bullet);
        firingInterval = Random.Range(0.75f,1.5f);
        Damage = Random.Range(1, 2)*10;
        Speed = Random.Range(10f, 20f);
        switchTime = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (switchTime > 0)
        {
            switchTime -= Time.deltaTime;
            if (switchTime <= 0)
            {
                typeOfFire = !typeOfFire;
                switchTime = 8;
            }
        }
        
        if (currInterval > 0)
        {
            currInterval -= Time.deltaTime;
        }


        if (target != null)
        {
            RaycastHit2D raycastHit = Physics2D.Linecast(transform.position, target.position, 1 << LayerMask.NameToLayer("WallColider"));


            isShooting = raycastHit.collider == null;
        
        
            if (isShooting && currNbBullet == 0)
                currNbBullet = nbBullet;
            

            if (typeOfFire)
            {
                if (currTimeBetweenBullet > 0)
                    currTimeBetweenBullet -= Time.deltaTime;

                if (currNbBullet != 0 && currTimeBetweenBullet <= 0 && currInterval <= 0)
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
            else if (raycastHit.collider == null)
            {
                if (currInterval <= 0)
                {
                    if (_photonView.IsMine)
                    {
                        fireABullet();
                        currInterval = firingInterval;
                    }
                }
            }
        }
    }

    private new void fireABullet()
    {
        GameObject newBullet = PhotonNetwork.Instantiate(Bullet.name, transform.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().RandomatorSetup(target, Damage, Speed);
    }


}
