using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RocketeerManagement : MonoBehaviour
{
    public Transform target;
    //private Transform currTarget;

    public float firingInTerval;
    private float currfiringInterval;

    public float dashInterval;
    public float currDashInterval;

    public float speed;
    private float currSpeed;

    private Rigidbody2D _rigidbody2D;
    private RocketeerWeapon _rocketeerWeapon;

    private bool isMoving;
    public bool Detected;
    private bool targetSet;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rocketeerWeapon = GetComponent<RocketeerWeapon>();
        currfiringInterval = firingInTerval;
        currDashInterval = dashInterval;
        currSpeed = speed;
    }

    private void Update()
    {
        
        if (currfiringInterval > 0)
            currfiringInterval -= Time.deltaTime;

        if (currfiringInterval <= 0 && !isMoving)
        {
            _rocketeerWeapon.Fire();
            currfiringInterval = firingInTerval;
        }
        

        if (currDashInterval > 0 && !isMoving && PhotonView.Get(this).IsMine)
            currDashInterval -= Time.deltaTime;

        if (currDashInterval <= 0 && target != null)
        {
            Move();
            currDashInterval = dashInterval;
        }
        
        if (isMoving)
        {
            if (Detected)
                _rigidbody2D.velocity *= 0.99f;
            
            Vector2 test = _rigidbody2D.velocity;
            float speed = test.magnitude;
            if (speed < 1)
            {
                Detected = false;
                isMoving = false;
                currDashInterval = dashInterval;
                _rigidbody2D.velocity = Vector2.zero;
                
            }
        }
    }

    private void Move()
    {
        if (!Detected)
        {
            _rigidbody2D.velocity = (target.position - transform.position).normalized * currSpeed; 
            isMoving = true;
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     Detected = true;
    // }
}
