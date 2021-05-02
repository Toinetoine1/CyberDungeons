using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace AI.Map
{
    public class WallGenerator : MonoBehaviour
    {
        private const int Delta = 5;

        private List<Wall> walls;
        private Vector2[,] array = new Vector2[Delta * 2, Delta * 2];

        public void CreateWall(List<Vector2> positions, GameObject verticalWall, GameObject horizontalWall)
        {
            walls = new List<Wall>();
            
            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
            pool.ResourceCache.Add(verticalWall.name, verticalWall);
            pool.ResourceCache.Add(horizontalWall.name, horizontalWall);
            
            foreach (Vector2 pos in positions)
            {
                int x = (int) (pos.x / MapGenerator.sizeX) + Delta;
                int y = (int) (pos.y / MapGenerator.sizeY) + Delta;
                //Debug.Log("PUT  y: " + y + "   x:" + x);
                array[y, x] = pos;
            }
            array[Delta, Delta] = Vector2.down;

            PrintArray();

            foreach (Vector2 pos in positions)
            {
                if (pos == Vector2.zero && pos.x != 0 && pos.y != 0)
                    continue;

                int y = (int) (pos.y / MapGenerator.sizeY);
                int x = (int) (pos.x / MapGenerator.sizeX);
                Debug.Log("y: " + y + "   x:" + x);

                if (array[y - 1 + Delta, x + Delta] == Vector2.zero)
                {
                    //Debug.Log("Need wall on the bottom in: x:" + x + "  y:" + y);
                    walls.Add(new Wall(horizontalWall, new Vector2(pos.x, pos.y - MapGenerator.sizeY / 2), true));
                } 
                if (array[y + 1 + Delta, x + Delta] == Vector2.zero)
                {
                    //Debug.Log("Need wall on the top in: x:" + x + "  y:" + y);
                    walls.Add(new Wall(horizontalWall, new Vector2(pos.x, pos.y + MapGenerator.sizeY / 2), true));
                } 
                if (array[y + Delta, x - 1 + Delta] == Vector2.zero)
                {
                    //Debug.Log("Need wall on the left in: x:" + x + "  y:" + y);
                    walls.Add(new Wall(verticalWall, new Vector2(pos.x - MapGenerator.sizeX / 2, pos.y), true));
                }
                if (array[y + Delta, x + 1 + Delta] == Vector2.zero)
                {
                    // Debug.Log("Need wall on the right in: x:" + x + "  y:" + y);
                    walls.Add(new Wall(verticalWall, new Vector2(pos.x + MapGenerator.sizeX / 2, pos.y), true));
                }
            }
            
            AstarPath.active.Scan();
        }

        private void PrintArray()
        {
            string str = "";
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == Vector2.zero)
                        str += "O";
                    else
                        str += "X";
                }

                str += "\n";
            }

            Debug.Log(str);
        }
    }
}