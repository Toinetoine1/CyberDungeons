﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Game
{
    public class SniperManagement : EnnemyWeapon
    {
        public bool isAiming;
    
        public float aimtime;
        private float currAimtime;
    
        private LineRenderer _lineRenderer;
    
        void Start()
        {
            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
            if (!pool.ResourceCache.ContainsKey(Bullet.name))
                pool.ResourceCache.Add(Bullet.name, Bullet);
            isAiming = false;
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.enabled = false;
            _photonView = PhotonView.Get(this);
        }
        
        // Update is called once per frame
        void Update()
        {
            if (isAiming)
            {
                DrawLine();
                currAimtime -= Time.deltaTime;
                if (currAimtime <= 0)
                {
                    if (_photonView.IsMine)
                    {
                        fireABullet();
                        currAimtime = aimtime;
                    }
                }
            }
            else
            {
                _lineRenderer.enabled = false;
                currAimtime = aimtime;
            }
        }
    
        private void DrawLine()
        {
            if (transform != null && target != null)
            {
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.SetPosition(1, target.position);   
            }
        }
    }
}

