﻿using System.Collections.Generic;
using AI.Map;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        public static List<Map> maps = new List<Map>();
        
        private WallGenerator wallGenerator;

        public const int sizeX = 38;
        public const int sizeY = 27;

        [SerializeField] public List<GameObject> availableMaps;
        [SerializeField] public GameObject spawn;
        [SerializeField]
        public GameObject verticalWall;
        [SerializeField]
        public GameObject horizontalWall;
        
        private Random _random = new Random();

        void Start()
        {
            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
            foreach (GameObject prefab in availableMaps)
            {
                pool.ResourceCache.Add(prefab.name, prefab);
            }
            pool.ResourceCache.Add(spawn.name, spawn);
            pool.ResourceCache.Add(verticalWall.name, verticalWall);
            pool.ResourceCache.Add(horizontalWall.name, horizontalWall);
            
            if (!PhotonNetwork.IsMasterClient)
                return;
            wallGenerator = gameObject.AddComponent<WallGenerator>();
            generate();
        }
        
        void generate()
        {
            //On ajoute sur tous les clients une map vide en (0,0)
            PhotonNetwork.Instantiate(spawn.name, Vector2.zero, Quaternion.identity);
            maps.Add(new Map(true, Vector2.zero, verticalWall, horizontalWall));
            
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
            int numberOfMap = 6;
            for (int i = 0; i < numberOfMap; i++)
            {
                //On choisit aléatoirement une tilesmap
                GameObject prefabGameObject = availableMaps[_random.Next(availableMaps.Count)];
                
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
                PhotonNetwork.Instantiate(prefabGameObject.name, position, Quaternion.identity);
                maps.Add(new Map(false, position, verticalWall, horizontalWall));
            }
            
            //On génère les murs
            wallGenerator.CreateWall(positions, verticalWall, horizontalWall);
        }
    }
}