using System.Collections.Generic;
using AI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = System.Random;

namespace Map
{
    public class TriggerEnemyArea : MonoBehaviour
    {
        private const int distanceToSpawn = 15;
        private const int delta = 12;

        public List<GameObject> mobs;
        public Transform Spawner;
        public bool hasSpawned;

        public static int aliveMob;

        private void Awake()
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
                aliveMob = hasToSpawn;
                Map map = Map.FindMapByVector(position);
                map.SpawnWall();

                if (PhotonNetwork.MasterClient.NickName == other.name)
                {
                    gameObject.GetComponent<PhotonView>()
                        .RPC("TPPlayer", RpcTarget.Others, other.transform.position, other.name);
                }

                while (hasToSpawn != 0)
                {
                    float x = position.x +
                              random.Next((-MapGenerator.sizeX + delta) / 2, (MapGenerator.sizeX - delta) / 2);
                    float y = position.y +
                              random.Next((-MapGenerator.sizeY + delta) / 2, (MapGenerator.sizeY - delta) / 2);

                    bool ok = true;
                    Vector2 transformPosition = new Vector2(x, y);
                    foreach (GameObject player in PlayerConnect.players)
                    {
                        if (Vector3.Distance(player.transform.position, transformPosition) < distanceToSpawn)
                            ok = false;
                    }

                    if (Physics2D.Linecast(transformPosition, transformPosition,
                        1 << LayerMask.NameToLayer("WallColider")))
                    {
                        ok = false;
                    }

                    if (ok)
                    {
                        PhotonNetwork.Instantiate(mobs[random.Next(mobs.Count)].name, transformPosition,
                            Quaternion.identity);
                        Debug.Log("Mob spawn in x:" + x + "  y:" + y);
                        gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        hasSpawned = true;
                        hasToSpawn--;
                    }
                }
            }

            if (PhotonNetwork.MasterClient.NickName != other.name)
            {
                GameObject obj = GameObject.Find(PhotonNetwork.MasterClient.NickName);
                if (obj != null)
                    obj.transform.position = other.transform.position;
            }
        }

        [PunRPC]
        public void TPPlayer(Vector3 pos, string name)
        {
            foreach (Player pl in PhotonNetwork.CurrentRoom.Players.Values)
            {
                if (pl.NickName == name)
                    continue;

                GameObject find = GameObject.Find(pl.NickName);
                if (find != null)
                    find.transform.position = pos;
            }
        }
    }
}