using Photon.Pun;
using UnityEngine;

namespace Map
{
    public class Wall
    {
        private GameObject obj;
        private Vector2 pos;
        private bool isStatic;

        public Wall(GameObject obj, Vector2 pos, bool isStatic, MapGenerator mapGenerator)
        {
            this.pos = pos;
            this.isStatic = isStatic;
            
            this.obj = PhotonNetwork.Instantiate(obj.name, pos, Quaternion.identity);
            mapGenerator.gameObject.GetComponent<PhotonView>().RPC("ChangeWallParent", RpcTarget.All, this.obj.name);
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