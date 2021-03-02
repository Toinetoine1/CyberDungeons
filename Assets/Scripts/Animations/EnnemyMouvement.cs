using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyMouvement : MonoBehaviour
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
        InputKey();
        Move();
    }

    private void Move()
    {
        transform.Translate(Time.deltaTime * speed * direction);
        SetMovementAnim(direction);
    }
    
    // Si vous voulez faire bouger le mob avec les touche zqsd enelever les commentaire.
    
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
