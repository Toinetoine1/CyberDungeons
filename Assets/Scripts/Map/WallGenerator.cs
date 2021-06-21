using System;
using System.Collections.Generic;
using Map;
using Photon.Pun;
using UnityEngine;

namespace AI.Map
{
    public class WallGenerator : MonoBehaviour
    {
        private const int Delta = 12;

        private List<Wall> walls;
        private Vector2[,] array;

        public void CreateWall(List<Vector2> positions, GameObject verticalWall, GameObject horizontalWall,
            MapGenerator mapGenerator, List<Vector2> availablePositions)
        {
            walls = new List<Wall>();
            array = new Vector2[Delta * 2, Delta * 2];

            foreach (Vector2 pos in positions)
            {
                int x = (int) (pos.x / MapGenerator.sizeX) + Delta;
                int y = (int) (pos.y / MapGenerator.sizeY) + Delta;
                //Debug.Log("PUT  y: " + y + "   x:" + x);
                array[y, x] = pos;
            }

            array[Delta, Delta] = Vector2.down;
            
            //On spawn la salle du boss
            List<Vector2> avBoss = new List<Vector2>();

            for (var i = 0; i < availablePositions.Count; i++)
            {
                Vector2 pos = availablePositions[i];
                int x = (int) (pos.x / MapGenerator.sizeX);
                int y = (int) (pos.y / MapGenerator.sizeY);

                if (array[y + 1 + Delta, x + Delta] == Vector2.zero &&
                    array[y + Delta, x + 1 + Delta] == Vector2.zero && array[y + Delta, x - 1 + Delta] == Vector2.zero)
                {
                    //Top
                    avBoss.Add(pos);
                }
                else if (array[y - 1 + Delta, x + Delta] == Vector2.zero &&
                         array[y + Delta, x + 1 + Delta] == Vector2.zero &&
                         array[y + Delta, x - 1 + Delta] == Vector2.zero)
                {
                    //Bottom
                    avBoss.Add(pos);
                }
                else if (array[y - 1 + Delta, x + Delta] == Vector2.zero &&
                         array[y + 1 + Delta, x + Delta] == Vector2.zero &&
                         array[y + Delta, x + 1 + Delta] == Vector2.zero)
                {
                    //Right
                    avBoss.Add(pos);
                }
                else if (array[y + Delta, x - 1 + Delta] == Vector2.zero &&
                         array[y + 1 + Delta, x + Delta] == Vector2.zero &&
                         array[y - 1 + Delta, x + Delta] == Vector2.zero)
                {
                    //Left
                    avBoss.Add(pos);
                }
            }

            Vector2 bossPos = avBoss[0];
            for (int i = 1; i < avBoss.Count; i++)
            {
                if (Vector2.Distance(Vector2.zero, avBoss[i]) > Vector2.Distance(Vector2.zero, bossPos))
                {
                    bossPos = avBoss[i];
                }
            }

            positions.Add(bossPos);
            GameObject child = null;

            switch (MapGenerator.level)
            {
                case 1:
                    child = PhotonNetwork.Instantiate(mapGenerator.bossLvl1.name, bossPos, Quaternion.identity);
                    break;
                case 2:
                    child = PhotonNetwork.Instantiate(mapGenerator.bossLvl2.name, bossPos, Quaternion.identity);
                    break;
                case 3:
                    child = PhotonNetwork.Instantiate(mapGenerator.bossLvl3.name, bossPos, Quaternion.identity);
                    break;
            }

            mapGenerator.gameObject.GetComponent<PhotonView>().RPC("ChangeMapParent", RpcTarget.All, child.name);
            MapGenerator.maps.Add(new global::Map.Map(false, bossPos, verticalWall, horizontalWall, mapGenerator));
            array[(int) (bossPos.y / MapGenerator.sizeY) + Delta, (int) (bossPos.x / MapGenerator.sizeX + Delta)] = bossPos;

            PrintArray();

            //On spawn les murs
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
                    walls.Add(new Wall(horizontalWall, new Vector2(pos.x, pos.y - MapGenerator.sizeY / 2), true,
                        mapGenerator));
                }

                if (array[y + 1 + Delta, x + Delta] == Vector2.zero)
                {
                    //Debug.Log("Need wall on the top in: x:" + x + "  y:" + y);
                    walls.Add(new Wall(horizontalWall, new Vector2(pos.x, pos.y + MapGenerator.sizeY / 2), true,
                        mapGenerator));
                }

                if (array[y + Delta, x - 1 + Delta] == Vector2.zero)
                {
                    //Debug.Log("Need wall on the left in: x:" + x + "  y:" + y);
                    walls.Add(new Wall(verticalWall, new Vector2(pos.x - MapGenerator.sizeX / 2, pos.y), true,
                        mapGenerator));
                }

                if (array[y + Delta, x + 1 + Delta] == Vector2.zero)
                {
                    // Debug.Log("Need wall on the right in: x:" + x + "  y:" + y);
                    walls.Add(new Wall(verticalWall, new Vector2(pos.x + MapGenerator.sizeX / 2, pos.y), true,
                        mapGenerator));
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