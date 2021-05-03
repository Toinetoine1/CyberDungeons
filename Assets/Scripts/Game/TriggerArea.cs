using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public GameObject Prefab;
    public Transform Spawner;
    public bool haSpawned;

    private void Start()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        if (!pool.ResourceCache.ContainsKey(Prefab.name))
            pool.ResourceCache.Add(Prefab.name, Prefab);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!haSpawned && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(Prefab.name, Spawner.transform.position, Quaternion.identity);
            Debug.Log("TOUCHED");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            haSpawned = true;
        }
    }
}
