using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Photon.MainMenu
{
    public class PlayerListingsMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform content;
        [SerializeField] private PlayerListing playerListing;

        private List<PlayerListing> listings = new List<PlayerListing>();
        private RoomsCanvases _canvases;
        
        public void FirstInitialize(RoomsCanvases canvases)
        {
            _canvases = canvases;
        }
        
        public void GetCurrentRoomPlayers()
        {
            foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
            {
                AddPlayerListing(playerInfo.Value);
            }
        }

        public override void OnLeftRoom()
        {
            content.DestroyChildren();
        }

        private void AddPlayerListing(Player player)
        {
            PlayerListing listing = Instantiate(playerListing, content);

            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                listings.Add(listing);
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            AddPlayerListing(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            int index = listings.FindIndex(x => x.Player == otherPlayer);
            if (index != -1)
            {
                Destroy(listings[index].gameObject);
                listings.RemoveAt(index);
            }
        }
    }
}