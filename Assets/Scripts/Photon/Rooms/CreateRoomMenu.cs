﻿using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.Rooms
{
    public class CreateRoomMenu : MonoBehaviourPunCallbacks
    {

        [SerializeField] private Text roomName;

        public void onClickCreateRoom()
        {
            Debug.Log("okkk");
            if (!PhotonNetwork.IsConnected)
            {
                Debug.Log("Photon not connected when trying to create a room !");
                return;
            }
            
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 2;
        
            //If a client disconnects, this actor is inactive first and removed after this timeout. In milliseconds.
            options.PlayerTtl = 60000;
            options.EmptyRoomTtl = 0;

            PhotonNetwork.JoinOrCreateRoom(roomName.text, options, TypedLobby.Default);
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("Created room successfully.");
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("Room creation failed: "+message);
        }
    }
}
