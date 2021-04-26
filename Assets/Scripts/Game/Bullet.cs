using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 BulletDir;
    private float Speed;
    private int Damage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * Speed * BulletDir);
        if (WallDetector(transform.position))
            Destroy(this.gameObject);
    }

    public void Setup(float speed, int dmg, Vector2 dir)
    {
        Speed = speed;
        Damage = dmg;
        BulletDir = dir;
    }

    private bool WallDetector(Vector2 pos)
    {
        return Physics.Raycast(pos, BulletDir, 10f, 8);
    }

    /*private int fetchDamage()
    {
        GameObject player = GameObject.Find("Player");
        Component curr = player.GetComponent("WeaponManagement");
        return curr.
        
    }*/
}
