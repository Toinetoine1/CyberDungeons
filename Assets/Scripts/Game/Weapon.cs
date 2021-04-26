using System;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform pfBullet;
        
        public int Damage;
        public float speed;
        private int currAmmo;
        public int maxAmmo;
        public float reloadTime;

        private bool reloading;
        private float timeRemaining;
        

        private void Start()
        {
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

        public void fire()
        {
            if (currAmmo > 0)
            {
                Transform test = Instantiate(pfBullet, transform.position, Quaternion.identity);
                Vector2 BulletDir = transform.position - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
                    Input.mousePosition.y, -cam.transform.position.z)).normalized;
                test.GetComponent<Bullet>().Setup(speed,Damage,);
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