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

        private void Start()
        {
            currAmmo = maxAmmo;
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
               
        }
    }
}