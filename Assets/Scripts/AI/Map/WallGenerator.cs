using System;
using System.Collections;
using System.Collections.Generic;
using AI.Map;
using Photon.Pun;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    public GameObject verticalWall;
    public GameObject horizontalWall;

    private List<Wall> walls;
    private Vector2[,] array = new Vector2[20, 20];
    
    public void CreateWall(List<Vector2> positions)
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        pool.ResourceCache.Add(verticalWall.name, verticalWall);
        pool.ResourceCache.Add(horizontalWall.name, horizontalWall);

        foreach (Vector2 pos in positions)
        {
            array[(int) (pos.y / MapGenerator.sizeY), (int) (pos.x / MapGenerator.sizeX)] = pos;
        }

        foreach (Vector2 pos in positions)
        {
            if (pos == Vector2.zero)
                continue;

            int y = (int) (pos.y / MapGenerator.sizeY);
            int x = (int) (pos.x / MapGenerator.sizeX);


            if (array[y + 1, x] == Vector2.zero)
            {
                walls.Add(new Wall(horizontalWall, new Vector2(pos.x + MapGenerator.sizeX / 2, pos.y), true));
            }
        }
    }

}