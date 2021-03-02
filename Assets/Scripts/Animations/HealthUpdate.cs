using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpdate : MonoBehaviour
{
    private HealthSystem _healthSystem;
    public int health;
    
    // Start is called before the first frame update
    void Start()
    {
        _healthSystem = new HealthSystem(health);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("HealthBar").localScale = new Vector3(_healthSystem.getHealthPercentage(), 1);
    }
}
