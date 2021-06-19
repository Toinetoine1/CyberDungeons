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
        public int currAmmo;
        public int maxAmmo;
        public float reloadTime;

        private bool reloading;
        private float timeRemaining;
        
        

        private void Start()
        {
            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
            if (!pool.ResourceCache.ContainsKey(pfBullet.name))
                pool.ResourceCache.Add(pfBullet.name, pfBullet);
            currAmmo = maxAmmo;
        }

        private void Update()
        {
            if (reloading)
            {
                timeRemaining -= Time.deltaTime;
                if (timeRemaining <= 0)
                {
                    FindObjectOfType<AudioManager>().Play("Reload");
                    reloading = false;
                    currAmmo = maxAmmo;
                }
            }

            if (currAmmo == 0 && !reloading)
            {
                Reload();
            }
        }

        public void fire(Camera cam, Transform transform)
        {
            if (currAmmo > 0 && !reloading)
            {
                GameObject test = PhotonNetwork.Instantiate(pfBullet.name, transform.position, Quaternion.identity);
                Vector2 BulletDir = (cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, -cam.transform.position.z)) - transform.root.position).normalized;
                test.GetComponent<Bullet>().Setup(speed,Damage,BulletDir);
                currAmmo -= 1;
                switch (maxAmmo)
                {
                    case 8 :
                        FindObjectOfType<AudioManager>().Play("PistolAudio");
                        break;
                    case 30 :
                        FindObjectOfType<AudioManager>().Play("AssaultRifleSound");
                        break;
                    case 45 :
                        FindObjectOfType<AudioManager>().Play("TheBiggerGunSound");
                        break;
                    case 25 :
                        FindObjectOfType<AudioManager>().Play("AssaultRifleSound");
                        break;
                    case 5 :
                        FindObjectOfType<AudioManager>().Play("AssaultRifleSound");
                        break;
                    case 6 :
                        FindObjectOfType<AudioManager>().Play("TheBiggerGunSound");
                        break;
                    case 15 :
                        FindObjectOfType<AudioManager>().Play("TheBiggerGunSound");
                        break;
                    case 20 :
                        FindObjectOfType<AudioManager>().Play("AssaultRifleSound");
                        break;
                }
            }
            else if (!reloading)
                Reload();
        }

        public void Reload()
        {
            reloading = true;
            timeRemaining = reloadTime;
        }

    }
}