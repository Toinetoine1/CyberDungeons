using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    private Health _healthSystem;

    public void setup(Health healthSystem)
    {
        _healthSystem = healthSystem;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("Bar").localScale = new Vector3(_healthSystem.getHealthPercentage(), 1);
    }
}
