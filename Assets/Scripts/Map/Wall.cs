using Photon.Pun;
using UnityEngine;

namespace Map
{
    public class Wall
    {
        private GameObject obj;
        private Vector2 pos;
        private bool isStatic;

        public Wall(GameObject obj, Vector2 pos, bool isStatic)
        {
            this.pos = pos;
            this.isStatic = isStatic;
            
            GameObject parent = GameObject.Find("Walls");
            this.obj = PhotonNetwork.Instantiate(obj.name, pos, Quaternion.identity);
            this.obj.transform.parent = parent.transform;
            Debug.Log("Spawn wall in x:"+pos.x+"  y:"+pos.y);
        }

        public void Destroy()
        {
            if (!isStatic)
                PhotonNetwork.Destroy(obj);
        }

        public Vector2 GetPosition()
        {
            return pos;
        }
    }
}