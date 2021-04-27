using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun.UtilityScripts;
using TMPro;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public GameObject Prefab;
    public Transform Spawner;
    public bool haSpawned;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!haSpawned)
        {
            Instantiate(Prefab, Spawner.transform.position, Spawner.transform.rotation);
            Debug.Log("TOUCHED");
            haSpawned = true;
        }
    }
}
