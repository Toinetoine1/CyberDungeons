using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployMobs : MonoBehaviour
{
    public GameObject ThugPrefab;
    public GameObject SpeedyPrefab;
    public GameObject PiouPiouGuyPrefab;

    private Vector2 TileMapBounds;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void SpawnEnnemy()
    {
        Instantiate(ThugPrefab);
        Instantiate(SpeedyPrefab);
        Instantiate(PiouPiouGuyPrefab);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
