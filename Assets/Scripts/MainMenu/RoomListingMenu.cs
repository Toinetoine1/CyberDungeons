using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Photon.MainMenu
{
    public class RoomListingMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] public Transform content;
        [SerializeField] public RoomListing roomListing;

        public List<RoomListing> listings = new List<RoomListing>();
        private RoomsCanvases roomsCanvases;
        
        public void FirstInitialize(RoomsCanvases canvases)
        {
            roomsCanvases = canvases;
        }

        public override void OnJoinedRoom()
        {
            roomsCanvases.CurrentRoomCanvas.Show();
            content.DestroyChildren();
            listings.Clear();
        }


    }
}