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
        healthBar.setup(healthSystem);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            healthSystem.damage(1);
        if (Input.GetKey(KeyCode.E))
            healthSystem.heal(1);
        Debug.Log(healthSystem.gethealth());
    }
}
