using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector2 direction;

    private Animator Animator;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<PhotonView>().IsMine)
        {
            InputKey();
            Move();
        }
    }

    private void Move()
    {
        transform.Translate(Time.deltaTime * speed * direction);
        SetMovementAnim(direction);
    }

    public void InputKey()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.Z))
            direction += Vector2.up;

        if (Input.GetKey(KeyCode.D))
            direction += Vector2.right;
            
        if (Input.GetKey(KeyCode.Q))
            direction += Vector2.left;

        if (Input.GetKey(KeyCode.S))
            direction += Vector2.down;
    }

    private void SetMovementAnim(Vector2 dir)
    {
        Animator.SetFloat("xDir", dir.x);
        Animator.SetFloat("yDir", dir.y);
    }
}
