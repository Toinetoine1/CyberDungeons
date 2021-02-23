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

        private List<RoomListing> listings = new List<RoomListing>();
        
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (RoomInfo info in roomList)
            {
                //Remove from rooms list
                if (info.RemovedFromList)
                {
                    int index = listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                    if (index != -1)
                    {
                        Destroy(listings[index].gameObject);
                        listings.RemoveAt(index);
                    }
                }
                //Added to rooms list
                else
                {
                    RoomListing listing = Instantiate(roomListing, content);

                    if (listing != null)
                    {
                        listing.SetRoomInfo(info);
                        listings.Add(listing);
                    }
                }
            }
        }
    }
}