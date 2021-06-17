using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetection : MonoBehaviour
{
    public RocketeerManagement RocketeerManagement;

    private void OnTriggerEnter2D(Collider2D other)
    {
        RocketeerManagement.Detected = true;
    }
}
