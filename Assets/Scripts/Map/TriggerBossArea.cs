using System.Collections.Generic;
using AI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = System.Random;

namespace Map
{
    public class TriggerBossArea : MonoBehaviour
    {
        public GameObject boss_lvl1;
        public GameObject boss_lvl2;
        public GameObject boss_lvl3;
        public Transform Spawner;
        public bool hasSpawned;

        private void Start()
        {
            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;

            if (MapGenerator.level == 1)
            {
                if (boss_lvl1 != null && !pool.ResourceCache.ContainsKey(boss_lvl1.name))
                {
                    pool.ResourceCache.Add(boss_lvl1.name, boss_lvl1);
                }
            }
            else if (MapGenerator.level == 2)
            {
                if (boss_lvl2 != null && !pool.ResourceCache.ContainsKey(boss_lvl2.name))
                    pool.ResourceCache.Add(boss_lvl2.name, boss_lvl2);
            }
            else if (MapGenerator.level == 3)
            {
                if (boss_lvl3 != null && !pool.ResourceCache.ContainsKey(boss_lvl3.name))
                    pool.ResourceCache.Add(boss_lvl3.name, boss_lvl3);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!hasSpawned && PhotonNetwork.IsMasterClient)
            {
                Vector3 position = Spawner.position;
                Map map = Map.FindMapByVector(position);
                map.SpawnWall();

                if (PhotonNetwork.MasterClient.NickName == other.name)
                {
                    gameObject.GetComponent<PhotonView>()
                        .RPC("TPPlayer", RpcTarget.Others, other.transform.position, other.name);
                }

                string bossName = "";
                switch (MapGenerator.level)
                {
                    case 1:
                        bossName = boss_lvl1.name;
                        FindObjectOfType<AudioManager>().Play("Bosslvl1");
                        FindObjectOfType<AudioManager>().Stop("Level1");
                        break;    
                    case 2:
                        bossName = boss_lvl2.name;
                        FindObjectOfType<AudioManager>().Play("Bosslvl2");
                        FindObjectOfType<AudioManager>().Stop("Level2");
                        break;
                    case 3:
                        bossName = boss_lvl3.name;
                        FindObjectOfType<AudioManager>().Play("Bosslvl3");
                        FindObjectOfType<AudioManager>().Stop("Level3");
                        break;
                }
                
                PhotonNetwork.Instantiate(bossName, position, Quaternion.identity);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                Debug.LogWarning("spawn a boss !");
                hasSpawned = true;
            }

            if (PhotonNetwork.MasterClient.NickName != other.name)
            {
                GameObject obj = GameObject.Find(PhotonNetwork.MasterClient.NickName);
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

                GameObject.Find(pl.NickName).transform.position = pos;
            }
        }
    }
}