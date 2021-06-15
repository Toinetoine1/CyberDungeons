﻿using Map;
using Photon.Pun;
using UnityEngine;

namespace Game
{
    public class Health : MonoBehaviour
    {
        public bool randomHealth;
        public float health;
        private float maxHealth;
        public HealthBar healthBar;
        


        // Start is called before the first frame update
        void Start()
        {
            if (randomHealth)
                health = Random.Range(200, 1000);
            
            maxHealth = health;
            //si vous voulez voir la vie des mob, inserez l'object healthbar dans l'object du mob et inséré cet healthbar dans le script dans unity.
            if (healthBar != null) 
            {
                healthBar.setup(this);
            }
        }
        void Update()
        {
            if (health <= 0 && gameObject.GetComponent<PhotonView>().IsMine)
            {
                TriggerEnemyArea.aliveMob--;
                if (TriggerEnemyArea.aliveMob == 0)
                {
                    Map.Map map = Map.Map.FindMapByVector(gameObject.transform.position);
                    map.DeleteWall();
                }
                PhotonNetwork.Destroy(gameObject);
            }
        }

        public void takeDamageRPC(int Damage)
        {
            PhotonView photonView = PhotonView.Get(gameObject);
            photonView.RPC("takeDamage", RpcTarget.All, Damage);
        }

        [PunRPC]
        public void takeDamage(int damage)
        {
            health -= damage;
        }
    
        public float getHealthPercentage()
        {
            return health / maxHealth;
        }
    }
}
