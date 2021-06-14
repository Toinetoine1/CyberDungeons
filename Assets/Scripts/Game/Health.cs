using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    private float maxHealth;
    public HealthBar healthBar;
    
    
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        //si vous voulez voir la vie des mob, inserez l'object healthbar dans l'object du mob et inséré cet healthbar dans le script dans unity.
        if (healthBar != null) 
        {
            healthBar.setup(this);
        }
    }
    void Update()
    {
        if (health <= 0 && gameObject.GetComponent<PhotonView>().IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        Debug.Log(health);
    }

    public void takeDamageRPC(int Damage)
    {
        PhotonView photonView = PhotonView.Get(gameObject);
        photonView.RPC("takeDamage", RpcTarget.All, Damage);
    }

    [PunRPC]
    public void takeDamage(int damage)
    {
        health -= damage;
    }
    
    public float getHealthPercentage()
    {
        return health / maxHealth;
    }
}
