using System;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class Weapon : MonoBehaviour
    {
        public GameObject Bullet;
        
        public int Damage;
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
                GameObject.Instantiate(Bullet);
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