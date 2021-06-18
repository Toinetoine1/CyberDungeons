using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class PotatorRoll : MonoBehaviour
{
    public Transform target;

    public float initialSpeed;
    private float currSpeed;

    private Rigidbody2D _rigidbody2D;
    public State _state;

    public float rollCoolDown;
    private float currCoolDown;

    private bool Touched;
    
    public enum State
    {
        Normal,
        Acceleration,
        deceleration
    }

    private void Start()
    {
        _state = State.Normal;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        currSpeed = initialSpeed;
        currCoolDown = rollCoolDown;
    }

    private void Update()
    {
        if (currCoolDown > 0)
            currCoolDown -= Time.deltaTime;
        
        if (currCoolDown <= 0)
        {
            if (_state == State.Normal)
            {
                _state = State.Acceleration;
            }
            Roll();
        }
        Debug.Log("state = " + _state);


    }

    public void Roll()
    {
        Debug.Log("rolling");
        if (_state == State.Acceleration)
        {
            currSpeed += 0.05f;
            if (currSpeed >= 10)
                _state = State.deceleration;
        }

        if (_state == State.deceleration)
        {
            currSpeed -= 0.1f;
            if (currSpeed <= 0)
            {
                _state = State.Normal;
                currCoolDown = rollCoolDown;
                Touched = false;
            }
        }

        _rigidbody2D.velocity = (target.position - transform.position).normalized * currSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !Touched)
        {
            other.GetComponent<Health>().takeDamageRPC(20);
            Touched = true;
            _state = State.deceleration;
        }
        else
        {
            _state = State.deceleration;
        }
    }
}
