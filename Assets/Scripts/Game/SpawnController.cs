using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private void Start()
    {
        GamesEvents.current.OnColliderSpawnEvent += OnCollisionSpawn;
    }

    // Update is called once per frame
    void OnCollisionSpawn()
    {
        
    }
}
