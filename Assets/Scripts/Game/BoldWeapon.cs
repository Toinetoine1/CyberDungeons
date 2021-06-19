using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using Photon.Pun;
public class BoldWeapon : EnnemyWeapon
{
    private float angle = 30;

    private void Update()
    {
        if (currInterval > 0)
        {
            currInterval -= Time.deltaTime;
        }

        
        if (currInterval <= 0 && target != null &&!Physics2D.Linecast(transform.position, target.position, 1 << LayerMask.NameToLayer("WallColider")))
        {
            if (_photonView.IsMine)
            {
                fireABullet();
                currInterval = firingInterval;
            }
        }
    }
    
    private new void fireABullet()
    {
        float plusBulDirX = (target.position.x - transform.position.x) + Mathf.Sin((angle * Mathf.PI) / 180f);
        float plusBulDirY = (target.position.y - transform.position.y) + Mathf.Cos((angle * Mathf.PI) / 180f);
        Vector2 bulletPlusVect = new Vector2(plusBulDirX, plusBulDirY).normalized;
        
        float lessBulDirX = (target.position.x - transform.position.x) - Mathf.Sin((angle * Mathf.PI) / 180f);
        float lessBulDirY = (target.position.y - transform.position.y) - Mathf.Cos((angle * Mathf.PI) / 180f);
        Vector2 bulletLessVect = new Vector2(lessBulDirX, lessBulDirY).normalized;

        
        GameObject bulletToward = PhotonNetwork.Instantiate(Bullet.name, transform.position, Quaternion.identity);
        GameObject bulletPlus = PhotonNetwork.Instantiate(Bullet.name, transform.position, Quaternion.identity);
        GameObject bulletLess = PhotonNetwork.Instantiate(Bullet.name, transform.position, Quaternion.identity);
        
        bulletToward.GetComponent<Bullet>().EnemiSetup(target);
        bulletPlus.GetComponent<Bullet>().setupWithVector(bulletPlusVect);
        bulletLess.GetComponent<Bullet>().setupWithVector(bulletLessVect);
    }
}
