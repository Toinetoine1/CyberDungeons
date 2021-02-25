﻿using UnityEngine;

namespace Photon.MainMenu
{
    public class CurrentRoomCanvas : MonoBehaviour
    {
        private RoomsCanvases roomsCanvases;
        
        public void FirstInitialize(RoomsCanvases canvases)
        {
            roomsCanvases = canvases;
        }

        public void Show()
        {
            roomsCanvases.CreateOrJoinRoomCanvas.gameObject.SetActive(false);
            gameObject.SetActive(true);
            Debug.Log("bonsoir a tous !");
        }

        private void Hide()
        {
            gameObject.SetActive(false);   
        }
        
        
    }
}