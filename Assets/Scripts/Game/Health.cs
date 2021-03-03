using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public HealthBar healthBar;
    private HealthSystem healthSystem;
    
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
    //pour tester la vie d'un mob/joueur, enlever les commentaire dans l'update.
    void Update()
    {
        health = healthSystem.gethealth();
        
        if (Input.GetKey(KeyCode.A))
            healthSystem.damage(1);
        if (Input.GetKey(KeyCode.E))
            healthSystem.heal(1);
        Debug.Log(healthSystem.gethealth());
        
    }
}
