using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;

namespace AI.Map
{
    public class MapGenerator : MonoBehaviour
    {
        private const int sizeX = 38;
        private const int sizeY = 27;

        [SerializeField] 
        public GameObject PlayerPrefab;
        
        [SerializeField] 
        public List<GameObject> availableMaps;
        public GameObject camera;

        private Random _random = new Random();

        void Start()
        {
            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
            foreach (GameObject prefab in availableMaps)
            {
                pool.ResourceCache.Add(prefab.name, prefab);
            }
            //
            // if (!PhotonNetwork.IsMasterClient)
            //     return;
            generate();
        }

        void generate()
        {
            camera.GetComponent<Camera>().transform.position = new Vector3(2, -6.5f, -24.94f);
            // PhotonNetwork.Instantiate(availableMaps[_random.Next(availableMaps.Count)].name, new Vector2(0, 0),
            //     Quaternion.identity);
            PhotonNetwork.Instantiate(availableMaps[0].name, new Vector2(0, 0),
                Quaternion.identity);
            
            // List<Vector2> positions = new List<Vector2>();
            // Vector2 right = new Vector2(sizeX, 0);
            // Vector2 left = new Vector2(-sizeX, 0);
            // Vector2 up = new Vector2(0, sizeY);
            // Vector2 down = new Vector2(0, -sizeY);
            // positions.Add(right);
            // positions.Add(left);
            // positions.Add(up);
            // positions.Add(down);
            //
            // int numberOfMap = 3;
            // for (int i = 0; i < numberOfMap; i++)
            // {
            //     GameObject prefabbGameObject = availableMaps[_random.Next(availableMaps.Count)];
            //     
            //     string testPrefab = prefabbGameObject.name;
            //     Vector2 position = positions[_random.Next(positions.Count)];
            //
            //     positions.Remove(position);
            //     List<Vector2> newVectors = new List<Vector2>();
            //     newVectors.Add(new Vector2(position.x + sizeX, position.y));
            //     newVectors.Add(new Vector2(position.x - sizeX, position.y));
            //     newVectors.Add(new Vector2(sizeX, position.y + sizeY));
            //     newVectors.Add(new Vector2(sizeX, position.y - sizeY));
            //
            //     foreach (Vector2 newVector in newVectors)
            //     {
            //         int index = positions.FindIndex(x => x.Equals(newVector));
            //         if (index == -1)
            //         {
            //             if (newVector.x == 0 && newVector.y == 0)
            //                 continue;
            //             positions.Add(newVector);
            //         }
            //     }
            //
            //     PhotonNetwork.Instantiate(testPrefab, position, Quaternion.identity);
            // }
        }
    }
}