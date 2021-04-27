using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;

namespace AI.Map
{
    public class MapGenerator : MonoBehaviour
    {
        private const int sizeX = 38;
        private const int sizeY = 27;

        [SerializeField] public GameObject VerticalWall;

        [SerializeField] public List<GameObject> availableMaps;

        private Random _random = new Random();

        void Start()
        {
            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
            foreach (GameObject prefab in availableMaps)
            {
                pool.ResourceCache.Add(prefab.name, prefab);
            }
            pool.ResourceCache.Add(VerticalWall.name, VerticalWall);

            if (!PhotonNetwork.IsMasterClient)
                return;
            generate();
        }
        
        void generate()
        {
            PhotonNetwork.Instantiate(availableMaps[_random.Next(availableMaps.Count)].name, new Vector2(0, 0),
                Quaternion.identity);
        
            List<Vector2> positions = new List<Vector2>();
            positions.Add(new Vector2(0,0));
            
            List<Vector2> availablePositions = new List<Vector2>();
            Vector2 right = new Vector2(sizeX, 0);
            Vector2 left = new Vector2(-sizeX, 0);
            Vector2 up = new Vector2(0, sizeY);
            Vector2 down = new Vector2(0, -sizeY);
            availablePositions.Add(right);
            availablePositions.Add(left);
            availablePositions.Add(up);
            availablePositions.Add(down);
            
            int numberOfMap = 50;
            for (int i = 0; i < numberOfMap; i++)
            {
                GameObject prefabbGameObject = availableMaps[_random.Next(availableMaps.Count)];
                
                Vector2 position = availablePositions[_random.Next(availablePositions.Count)];
                
                positions.Add(position);
                availablePositions.Remove(position);
                List<Vector2> newVectors = new List<Vector2>();
                newVectors.Add(new Vector2(position.x + sizeX, position.y));
                newVectors.Add(new Vector2(position.x - sizeX, position.y));
                newVectors.Add(new Vector2(position.x, position.y + sizeY));
                newVectors.Add(new Vector2(position.x, position.y - sizeY));
                
                foreach (Vector2 newVector in newVectors)
                {
                    // int index = positions.FindIndex(vec => Math.Abs(vec.x - newVector.x) < 0.1 && Math.Abs(vec.y - newVector.y) < 0.1);
                    if (!positions.Contains(newVector) && !availablePositions.Contains(newVector))
                    {
                        if (newVector.x == 0 && newVector.y == 0)
                            continue;
                        
                        availablePositions.Add(newVector);
                    }
                }
                
                // Debug.Log("New map in: x:"+position.x/sizeX+"  y:"+position.y/sizeY);
                // StringBuilder str = new StringBuilder();
                // foreach (var VARIABLE in availablePositions)
                // {
                //     str.Append("(" + VARIABLE.x / sizeX + "," + VARIABLE.y / sizeY+") ");
                // }
                // Debug.Log("AvailablePositions: "+str);
                //
                // str.Length = 0;
                // foreach (var VARIABLE in positions)
                // {
                //     str.Append("(" + VARIABLE.x / sizeX + "," + VARIABLE.y / sizeY+") ");
                // }
                // Debug.Log("Positions: "+str);
                //
                // Debug.Log(prefabbGameObject.name);
                PhotonNetwork.Instantiate(prefabbGameObject.name, position, Quaternion.identity);
                PhotonNetwork.Instantiate(VerticalWall.name, position, Quaternion.identity);
            }
        }
    }
}