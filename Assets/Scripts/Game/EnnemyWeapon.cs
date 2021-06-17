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

    public float firingInterval;
    protected float currInterval;
    
    public Transform target;

    protected PhotonView _photonView;


    private void Start()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        if (!pool.ResourceCache.ContainsKey(Bullet.name))
            pool.ResourceCache.Add(Bullet.name, Bullet);
        _photonView = PhotonView.Get(this);
    }

    private void Update()
    {
        if (currInterval > 0)
        {
            currInterval -= Time.deltaTime;
        }

        if (currInterval <= 0 && !Physics2D.Linecast(transform.position, target.position, 1 << LayerMask.NameToLayer("WallColider")))
        {
            _photonView.RPC("fireABullet", RpcTarget.All);
        }
    }
    

    [PunRPC]
    protected void fireABullet()
    {
        GameObject newBullet = PhotonNetwork.Instantiate(Bullet.name, transform.position, Quaternion.identity);
        if (this is SniperManagement)
            newBullet.GetComponent<Bullet>().EnemiSniperSetup(target);
        else
            newBullet.GetComponent<Bullet>().EnemiSetup(target);
    }


}
