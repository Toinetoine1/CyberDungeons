using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private HealthSystem _healthSystem;
    private void Start()
    {
        _healthSystem = 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            HealthSystem.damage(10);
        if (Input.GetKey(KeyCode.E))
            HealthSystem.heal(10);
    }
}
