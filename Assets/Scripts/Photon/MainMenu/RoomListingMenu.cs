using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Photon.MainMenu
{
    public class RoomListingMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform content;
        [SerializeField] private RoomListing roomListing;
        
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (RoomInfo info in roomList)
            {
                RoomListing listing = Instantiate(roomListing, content);
                if (listing != null)
                {
                    listing.SetRoomInfo(info);
                    Debug.Log("okkkk");
                }
            }
        }
    }
}
