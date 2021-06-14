using System.Collections.Generic;
using AI.Map;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class TriggerArea : MonoBehaviour
{
    private const int distanceToSpawn = 15;
    private const int delta = 12;
    
    public List<GameObject> mobs;
    public Transform Spawner;
    public bool hasSpawned;
    public TilemapCollider2D tilemapCollider2D;

    private void Start()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;

        foreach (GameObject prefab in mobs)
        {
            if (!pool.ResourceCache.ContainsKey(prefab.name))
                pool.ResourceCache.Add(prefab.name, prefab);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasSpawned && PhotonNetwork.IsMasterClient)
        {
            Vector3 position = Spawner.position;
            Random random = new Random();
            int hasToSpawn = random.Next(2, 6);
            
            while (hasToSpawn != 0)
            {
                float x = position.x + random.Next((-MapGenerator.sizeX + delta) / 2, (MapGenerator.sizeX - delta) / 2);
                float y = position.y + random.Next((-MapGenerator.sizeY + delta) / 2, (MapGenerator.sizeY - delta) / 2);

                bool ok = true;
                Vector2 transformPosition = new Vector2(x, y);
                foreach (GameObject player in PlayerConnect.players)
                {
                    if (Vector3.Distance(player.transform.position, transformPosition) < distanceToSpawn)
                        ok = false;
                }

                Vector2 clone = new Vector2(x + 1, y);
                if (Physics2D.Linecast(transformPosition, clone, 1 << LayerMask.NameToLayer("WallColider")))
                {
                    Debug.Log("touching colliderr");
                    ok = false;
                }

                Debug.Log(ok);
                
                if (ok)
                {
                    PhotonNetwork.Instantiate(mobs[random.Next(mobs.Count)].name, transformPosition, Quaternion.identity);
                    Debug.Log("Mob spawn in x:"+x+"  y:"+y);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    hasSpawned = true;
                    hasToSpawn--;
                }
            }
        }
    }
}
