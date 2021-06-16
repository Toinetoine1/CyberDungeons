using System.Collections;
using System.Collections.Generic;
using AI.Map;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = System.Random;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        public static List<Map> maps = new List<Map>();
        
        private WallGenerator wallGenerator;
        private int level;

        public const int sizeX = 38;
        public const int sizeY = 27;
        
        [SerializeField] public List<GameObject> availableMapsLvl1;
        [SerializeField] public List<GameObject> availableMapsLvl2;
        
        [SerializeField] public GameObject spawnLvl1;
        [SerializeField] public GameObject bossLvl1;
        
        [SerializeField] public GameObject spawnLvl2;
        [SerializeField] public GameObject bossLvl2;
        
        [SerializeField]
        public GameObject verticalWall;
        [SerializeField]
        public GameObject horizontalWall;
        
        private Random _random = new Random();

        void Start()
        {
            level = 1;
            
            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
            foreach (GameObject prefab in availableMapsLvl1)
            {
                pool.ResourceCache.Add(prefab.name, prefab);
            }
            foreach (GameObject prefab in availableMapsLvl2)
            {
                pool.ResourceCache.Add(prefab.name, prefab);
            }
            pool.ResourceCache.Add(verticalWall.name, verticalWall);
            pool.ResourceCache.Add(horizontalWall.name, horizontalWall);
            
            pool.ResourceCache.Add(spawnLvl1.name, spawnLvl1);
            pool.ResourceCache.Add(bossLvl1.name, bossLvl1);
            
            pool.ResourceCache.Add(spawnLvl2.name, spawnLvl2);
            pool.ResourceCache.Add(bossLvl2.name, bossLvl2);
            
            if (!PhotonNetwork.IsMasterClient)
                return;
            wallGenerator = gameObject.AddComponent<WallGenerator>();
            generate();
        }
        
        void generate()
        {
            if (!PhotonNetwork.IsMasterClient)
                return;
            
            Debug.LogWarning("GENERATE MAPS");

            GameObject child = null;
            //On ajoute sur tous les clients une map vide en (0,0)
            switch (level)
            {
                case 1:
                    child = PhotonNetwork.Instantiate(spawnLvl1.name, Vector2.zero, Quaternion.identity);
                    break;
                case 2:
                    child = PhotonNetwork.Instantiate(spawnLvl2.name, Vector2.zero, Quaternion.identity);
                    break;
            }
            gameObject.GetComponent<PhotonView>().RPC("ChangeMapParent", RpcTarget.All, child.name);

            maps.Add(new Map(true, Vector2.zero, verticalWall, horizontalWall, this));
            
            //Positions de toutes les tilesmap
            List<Vector2> positions = new List<Vector2>();
            positions.Add(Vector2.zero);
            
            //Positions disponibles pour générer la prochaine map
            List<Vector2> availablePositions = new List<Vector2>();
            
            //On ajoute toutes les positions adjacentes
            availablePositions.Add(new Vector2(sizeX, 0));
            availablePositions.Add(new Vector2(-sizeX, 0));
            availablePositions.Add(new Vector2(0, sizeY));
            availablePositions.Add(new Vector2(0, -sizeY));
            
            //Nombre de tilesmap a placer
            int numberOfMap = new Random().Next(6, 12);
            for (int i = 0; i < numberOfMap; i++)
            {
                //On choisit aléatoirement une tilesmap
                GameObject prefabGameObject = null;
                switch (level)
                {
                    case 1:
                        prefabGameObject = availableMapsLvl1[_random.Next(availableMapsLvl1.Count)];
                        break;
                    case 2:
                        prefabGameObject = availableMapsLvl2[_random.Next(availableMapsLvl2.Count)];
                        break;
                }
                
                //On choisit aléatoirement une position
                Vector2 position = availablePositions[_random.Next(availablePositions.Count)];
                
                positions.Add(position);
                availablePositions.Remove(position);
                
                //On crée et ajoute si possible les positions adjacentes
                List<Vector2> newVectors = new List<Vector2>();
                newVectors.Add(new Vector2(position.x + sizeX, position.y));
                newVectors.Add(new Vector2(position.x - sizeX, position.y));
                newVectors.Add(new Vector2(position.x, position.y + sizeY));
                newVectors.Add(new Vector2(position.x, position.y - sizeY));
                
                foreach (Vector2 newVector in newVectors)
                {
                    if (!positions.Contains(newVector) && !availablePositions.Contains(newVector))
                    {
                        if (newVector.x == 0 && newVector.y == 0)
                            continue;
                        
                        availablePositions.Add(newVector);
                    }
                }
                
                //On ajoute notre tilesmap sur tous les clients
                child = PhotonNetwork.Instantiate(prefabGameObject.name, position, Quaternion.identity);
                gameObject.GetComponent<PhotonView>().RPC("ChangeMapParent", RpcTarget.All, child.name);
                
                maps.Add(new Map(false, position, verticalWall, horizontalWall, this));
            }
            
            //On spawn la salle du boss
            Vector2 bossPos = availablePositions[_random.Next(availablePositions.Count)];
            positions.Add(bossPos);

            switch (level)
            {
                case 1:
                    child = PhotonNetwork.Instantiate(bossLvl1.name, bossPos, Quaternion.identity);
                    break;
                case 2:
                    child = PhotonNetwork.Instantiate(bossLvl2.name, bossPos, Quaternion.identity);
                    break;
            }
            gameObject.GetComponent<PhotonView>().RPC("ChangeMapParent", RpcTarget.All, child.name);
            
            maps.Add(new Map(false, bossPos, verticalWall, horizontalWall, this));
            
            //On génère les murs
            wallGenerator.CreateWall(positions, verticalWall, horizontalWall, this);
            gameObject.GetComponent<PhotonView>().RPC("TPPlayer", RpcTarget.All);
        }

        [PunRPC]
        public void TPPlayer()
        {
            foreach (Player pl in PhotonNetwork.CurrentRoom.Players.Values)
            {
                GameObject.Find(pl.NickName).transform.position = Vector3.zero;
            }
        }

        public void nextLevel()
        {
            level++;
            gameObject.GetComponent<PhotonView>().RPC("DeleteAll", RpcTarget.All);
            maps.Clear();

            StartCoroutine(GenerateNewMap());
        }

        private IEnumerator GenerateNewMap()
        {
            yield return new WaitForSeconds(4);
            
            Debug.LogWarning("KIKIKIKIIIIII");
            generate();
            
        }

        [PunRPC]
        public void ChangeMapParent(string gameObject)
        {
            Debug.LogWarning("change parent of: "+gameObject);
            GameObject parent = GameObject.Find("Maps");
            GameObject child = GameObject.Find(gameObject);
            child.name = "Map" + _random.Next(0, 99999);
            child.transform.parent = parent.transform;
        }
        
        [PunRPC]
        public void ChangeWallParent(string gameObject)
        {
            Debug.LogWarning("change parent of: "+gameObject);
            GameObject parent = GameObject.Find("Walls");
            GameObject child = GameObject.Find(gameObject);
            child.name = "Wall" + _random.Next(0, 99999);
            child.transform.parent = parent.transform;
        }

        [PunRPC]
        public void DeleteAll()
        {
            GameObject maps = GameObject.Find("Maps");
            GameObject walls = GameObject.Find("Walls");
            Destroy(maps);
            Destroy(walls);
            
            Instantiate(new GameObject("Maps"));
            Instantiate(new GameObject("Walls"));
        }
    }
}