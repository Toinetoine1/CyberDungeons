using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public HealthBar healthBar;
    public HealthSystem healthSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(health);
        //si vous voulez voir la vie des mob, inserez l'object healthbar dans l'object du mob et inséré cet healthbar dans le script dans unity.
        if (healthBar != null) 
        {
            healthBar.setup(healthSystem);
        }
    }
    void Update()
    {
        health = healthSystem.gethealth();
        if (health <= 0)
            PhotonNetwork.Destroy(gameObject);
    }

    public void takeDamage(int Damage)
    {
        healthSystem.damage(Damage);
    }
}
