using System;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject pfBullet;
        
        public int Damage;
        public float speed;
        private int currAmmo;
        public int maxAmmo;
        public float reloadTime;

        private bool reloading;
        private float timeRemaining;
        
        

        private void Start()
        {
            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
            pool.ResourceCache.Add(pfBullet.name, pfBullet);
            currAmmo = maxAmmo;
        }

        private void Update()
        {
            if (reloading)
            {
                timeRemaining -= Time.deltaTime;
                if (reloadTime <= 0)
                {
                    reloading = false;
                    currAmmo = maxAmmo;
                }
            }
        }

        public void fire(Camera cam, Transform transform)
        {
            if (currAmmo > 0)
            {
                GameObject test = PhotonNetwork.Instantiate(pfBullet.name, transform.position, Quaternion.identity);
                Vector2 BulletDir = (cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, -cam.transform.position.z)) - transform.root.position).normalized;
                Debug.Log(BulletDir);
                test.GetComponent<Bullet>().Setup(speed,Damage,BulletDir);
            }
            else
                Reload();
        }

        public void Reload()
        {
            reloading = true;
            timeRemaining = reloadTime;
        }

    }
}