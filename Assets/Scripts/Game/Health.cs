using AI;
using Map;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Game
{
    public class Health : MonoBehaviour
    {
        public static int alivePlayer;
        
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
                if (gameObject.CompareTag("Boss"))
                {
                    switch (MapGenerator.level)
                    {
                        case 1 :
                            FindObjectOfType<AudioManager>().Play("Level2");
                            FindObjectOfType<AudioManager>().Stop("Bosslvl1");
                            break;
                        case 2 :
                            FindObjectOfType<AudioManager>().Play("Level3");
                            FindObjectOfType<AudioManager>().Stop("Bosslvl2");
                            break;
                    }

                    PhotonView.Get(this).RPC("GiveMaxHealthRPC", RpcTarget.All);
                    MapGenerator mapGenerator = FindObjectOfType<MapGenerator>();
                    mapGenerator.nextLevel();
                    
                } else if (gameObject.CompareTag("Enemy"))
                {
                    TriggerEnemyArea.aliveMob--;
                    if (TriggerEnemyArea.aliveMob == 0)
                    {
                        Map.Map map = Map.Map.FindMapByVector(gameObject.transform.position);
                        map.DeleteWall();
                    }   
                } else if (gameObject.CompareTag("Player"))
                {
                    if (PhotonNetwork.CurrentRoom.Players.Values.Count == 1)
                    {
                        PhotonNetwork.LoadLevel(0);
                        PhotonNetwork.LeaveRoom();
                        PlayerConnect.hasAlreadyPlayed = true;
                    }
                    else
                    {
                        PhotonView photonView = gameObject.GetComponent<PhotonView>();
                        if (alivePlayer == 2)
                        {
                            foreach (Player pl in PhotonNetwork.CurrentRoom.Players.Values)
                            {
                                if(pl.NickName == gameObject.name)
                                    continue;
                                GameObject otherPlayerName = GameObject.Find(pl.NickName);
                                otherPlayerName.transform.GetChild(1).gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            photonView.RPC("LeaveGame", RpcTarget.All);
                        }
                        
                        photonView.RPC("OnPlayerDeath", RpcTarget.All);
                    }
                }
                
                PhotonNetwork.Destroy(gameObject);
            }
        }

        [PunRPC]
        public void LeaveGame()
        {
            PlayerConnect.hasAlreadyPlayed = true;
            PhotonNetwork.LoadLevel(0);
            PhotonNetwork.LeaveRoom();
        }
        
        [PunRPC]
        public void OnPlayerDeath()
        {
            alivePlayer--;
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


        [PunRPC]
        private void GiveMaxHealthRPC()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in players)
            {
                player.GetComponent<Health>().GiveMaxHealth();
            }
        }
        
        private void GiveMaxHealth()
        {
            health = maxHealth;
        }
    }
}
