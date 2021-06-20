using System;
using Map;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Game
{
    public class CheatCode : MonoBehaviour
    {
        public static bool tp;
        public static bool kill;

        private void Start()
        {
            tp = false;
            kill = false;
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.P) && !tp)
            {
                tp = true;
                Vector3 spawnerPosition = FindObjectOfType<TriggerBossArea>().Spawner.position;
                Vector3 toSpawn = new Vector3(spawnerPosition.x - 10, spawnerPosition.y, spawnerPosition.z);
                gameObject.GetComponent<PhotonView>().RPC("TeleportToBoss", RpcTarget.All, toSpawn);
            }

            if (Input.GetKey(KeyCode.M) && !kill)
            {
                GameObject boss = GameObject.FindWithTag("Boss");
                if (boss != null)
                {
                    kill = true;
                    boss.GetComponent<Health>().health = -1;
                }
            }
            
        }

        [PunRPC]
        public void TeleportToBoss(Vector3 pos)
        {
            tp = true;
            foreach (Player pl in PhotonNetwork.CurrentRoom.Players.Values)
            {
                GameObject obj = GameObject.Find(pl.NickName);
                obj.transform.position = pos;
            }
        }
    }
}
