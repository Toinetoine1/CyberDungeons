using AI.Map;
using UnityEngine;

namespace Map
{
    public class Map
    {
        private bool spawn;
        private Vector2 pos { get; }
        private Wall top { get; set; }
        private Wall bottom { get; set; }
        private Wall right { get; set; }
        private Wall left { get; set; }

        private GameObject verticalWall;
        private GameObject horizontalWall;
        private MapGenerator _mapGenerator;
        
        public Map(bool spawn, Vector2 pos, GameObject verticalWall, GameObject horizontalWall,
            MapGenerator mapGenerator)
        {
            this.verticalWall = verticalWall;
            this.horizontalWall= horizontalWall;
            this.spawn = spawn;
            this.pos = pos;
            _mapGenerator = mapGenerator;
        }

        public void SpawnWall()
        {
            if(!spawn)
            {
                bottom = new Wall(horizontalWall, new Vector2(pos.x, pos.y - MapGenerator.sizeY / 2), false,_mapGenerator);
                top = new Wall(horizontalWall, new Vector2(pos.x, pos.y + MapGenerator.sizeY / 2), false, _mapGenerator);
                left = new Wall(verticalWall, new Vector2(pos.x - MapGenerator.sizeX / 2, pos.y), false, _mapGenerator);
                right = new Wall(verticalWall, new Vector2(pos.x + MapGenerator.sizeX / 2, pos.y), false, _mapGenerator);
            }
        }

        public void DeleteWall()
        {
            bottom.Destroy();
            top.Destroy();
            left.Destroy();
            right.Destroy();
        }

        public static Map FindMapByVector(Vector2 vector2)
        {
            if (MapGenerator.maps.Count == 0)
                return null;
            if (MapGenerator.maps.Count == 1)
                return MapGenerator.maps[0];
            
            Map findedMap = MapGenerator.maps[0];

            foreach (Map map in MapGenerator.maps)
            {
                if (Vector2.Distance(vector2, map.pos) < Vector2.Distance(vector2, findedMap.pos))
                {
                    findedMap = map;
                }
            }
            
            return findedMap;
        }
    }
}
