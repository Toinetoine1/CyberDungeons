using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> availableMaps;

    private Random _random = new Random();
    
    void Start()
    {
        //PhotonNetwork.Instantiate()   
    }
}
