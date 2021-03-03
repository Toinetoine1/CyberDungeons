using System.Collections.Generic;
using Photon.MainMenu;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomListingUpdate : MonoBehaviourPunCallbacks
{
    [SerializeField] private RoomListingMenu _roomListingMenu;
    
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            //Remove from rooms list
            if (info.RemovedFromList)
            {
                int index = _roomListingMenu.listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(_roomListingMenu.listings[index].gameObject);
                    _roomListingMenu.listings.RemoveAt(index);
                }
            }
            //Added to rooms list
            else
            {
                int index = _roomListingMenu.listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index == -1)
                {
                    RoomListing listing = Instantiate(_roomListingMenu.roomListing, _roomListingMenu.content);

                    if (listing != null)
                    {
                        listing.SetRoomInfo(info);
                        _roomListingMenu.listings.Add(listing);
                    }    
                }
                else
                {
                        
                }
                    
            }
        }
    }
}
