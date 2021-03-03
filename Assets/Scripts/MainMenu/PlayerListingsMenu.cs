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

        public override void OnEnable()
        {
            base.OnEnable();
            GetCurrentRoomPlayers();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            for (int i = 0; i < listings.Count; i++)
            {
                Destroy(listings[i].gameObject);
            }
            
            listings.Clear();
        }

        public void GetCurrentRoomPlayers()
        {
            if(!PhotonNetwork.IsConnected || PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
                return;
            
            foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
            {
                AddPlayerListing(playerInfo.Value);
            }
        }

        private void AddPlayerListing(Player player)
        {
            int index = listings.FindIndex(x => x.Player == player);
            if (index != -1)
            {
                listings[index].SetPlayerInfo(player);
            }
            else
            {
                PlayerListing listing = Instantiate(playerListing, content);

                if (listing != null)
                {
                    listing.SetPlayerInfo(player);
                    listings.Add(listing);
                }
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

        public void OnClickStartGame()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.LoadLevel(1);
            }
        }
    }
}