using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketeerManagement : MonoBehaviour
{
    public Transform target;

    public float firingInTerval;
    private float currfiringInterval;

    public float dashInterval;
    private float currDashInterval;

    public float speed;
    private float currSpeed;

    private Rigidbody2D _rigidbody2D;

    private bool isMoving;
    public bool Detected;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        currfiringInterval = firingInTerval;
        currDashInterval = dashInterval;
        isMoving = false;
        currSpeed = speed;
    }

    private void Update()
    {
        Debug.Log(Detected);
        if (currfiringInterval > 0)
            currfiringInterval -= Time.deltaTime;

        if (currDashInterval > 0)
            currDashInterval -= Time.deltaTime;


        
        if (currDashInterval <= 0 && !Detected)
            Move();
        
        if (isMoving)
        {
            currSpeed -= 0.25f;

            if (Detected)
                _rigidbody2D.velocity *= 0.75f;
                
            if (currSpeed == 0)
            {
                _rigidbody2D.velocity = Vector2.zero;
                isMoving = false;
                Detected = false;
                currSpeed = speed;
                currDashInterval = dashInterval;
            }
        }

        
    }

    private void Move()
    {
        _rigidbody2D.velocity = (target.position - transform.position) * currSpeed; 
        isMoving = true;
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     Detected = true;
    // }
}
