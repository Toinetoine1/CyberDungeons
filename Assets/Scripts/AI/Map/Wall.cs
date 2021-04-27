﻿using Photon.Pun;
using UnityEngine;

namespace AI.Map
{
    public class Wall : MonoBehaviour
    {
        private GameObject obj;
        private Vector2 pos;
        private bool isStatic;

        public Wall(GameObject obj, Vector2 pos, bool isStatic)
        {
            this.obj = obj;
            this.pos = pos;
            this.isStatic = isStatic;

            PhotonNetwork.Instantiate(obj.name, pos, Quaternion.identity);
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