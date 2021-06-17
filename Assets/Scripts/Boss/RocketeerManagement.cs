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

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        currfiringInterval = firingInTerval;
    }

    private void Update()
    {
        if (currfiringInterval > 0)
        {
            currfiringInterval -= Time.deltaTime;
        }

        if (currDashInterval > 0)
        {
            currDashInterval -= Time.deltaTime;
        }

        if (currDashInterval <= 0)
        {
            Move();
        }
    }

    private void Move()
    {
        _rigidbody2D.velocity = target.position;
    }
    
    
}
