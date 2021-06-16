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
        public bool isDead = false;
        public bool hasSpawned;

        private void Start()
        {
            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;

            if (boss_lvl1 != null)
                pool.ResourceCache.Add(boss_lvl1.name, boss_lvl1);

            if (boss_lvl2 != null)
                pool.ResourceCache.Add(boss_lvl2.name, boss_lvl2);

            if (boss_lvl3 != null)
                pool.ResourceCache.Add(boss_lvl3.name, boss_lvl3);
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
                    gameObject.GetComponent<PhotonView>().RPC("TPPlayer", RpcTarget.Others, other.transform.position, other.name);
                }

                PhotonNetwork.Instantiate(boss_lvl1.name, position, Quaternion.identity);
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