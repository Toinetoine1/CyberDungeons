using System;
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

        public List<GameObject> availableMaps;
        public GameObject camera;

        private Random _random = new Random();

        void Start()
        {
            if (!PhotonNetwork.IsMasterClient)
                return;

            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
            foreach (GameObject prefab in availableMaps)
            {
                pool.ResourceCache.Add(prefab.name, prefab);
            }

            camera.GetComponent<Camera>().transform.position = new Vector3(2, -6.5f, -24.94f);
            PhotonNetwork.Instantiate(availableMaps[_random.Next(availableMaps.Count)].name, new Vector2(0, 0),
                Quaternion.identity);

            List<Vector2> positions = new List<Vector2>();
            Vector2 right = new Vector2(sizeX, 0);
            Vector2 left = new Vector2(-sizeX, 0);
            Vector2 up = new Vector2(0, sizeY);
            Vector2 down = new Vector2(0, -sizeY);
            positions.Add(right);
            positions.Add(left);
            positions.Add(up);
            positions.Add(down);

            int numberOfMap = 10;
            for (int i = 0; i < numberOfMap; i++)
            {
                string testPrefab = availableMaps[_random.Next(availableMaps.Count)].name;
                Vector2 position = positions[_random.Next(positions.Count)];

                positions.Remove(position);
                List<Vector2> newVectors = new List<Vector2>();
                newVectors.Add(new Vector2(position.x + sizeX, position.y));
                newVectors.Add(new Vector2(position.x - sizeX, position.y));
                newVectors.Add(new Vector2(sizeX, position.y + sizeY));
                newVectors.Add(new Vector2(sizeX, position.y - sizeY));

                foreach (Vector2 newVector in newVectors)
                {
                    int index = positions.FindIndex(x => x.Equals(newVector));
                    if (index == -1)
                    {
                        if(newVector.x == 0 && newVector.y == 0)
                            continue;
                        Debug.Log("add newVector");
                        positions.Add(newVector);
                    }
                }

                PhotonNetwork.Instantiate(testPrefab, position, Quaternion.identity);
            }
        }
    }
}