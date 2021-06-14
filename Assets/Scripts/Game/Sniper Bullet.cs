using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : Bullet
{
    public new void EnemiSetup(Transform targetTransform)
    {
        Speed = 15;
        Damage = 10;
        BulletDir = (targetTransform.position - transform.position).normalized;
        Friendly = false;
    }
}
