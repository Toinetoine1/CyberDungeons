using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    private HealthSystem _healthSystem;

    public void setup(HealthSystem healthSystem)
    {
        _healthSystem = healthSystem;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("Bar").localScale = new Vector3(_healthSystem.getHealthPercentage(), 1);
    }
}
