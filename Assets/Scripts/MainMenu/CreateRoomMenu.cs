using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.MainMenu
{
    public class CreateRoomMenu : MonoBehaviourPunCallbacks
    {
        
        [SerializeField] private Text roomName;

        private RoomsCanvases roomsCanvases;
        
        public void FirstInitialize(RoomsCanvases canvases)
        {
            roomsCanvases = canvases;
        }

        public void onClickCreateRoom()
        {
            if (!PhotonNetwork.IsConnected)
            {
                Debug.Log("Photon not connected when trying to create a room !");
                return;
            }

            if (roomName.text == "")
            {
                Debug.Log("RoomName not set !");
                return;
            }
            
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 2;
        
            //If a client disconnects, the player will be inactive first and removed after this timeout. In milliseconds.
            options.PlayerTtl = 0;
            //If a room is empty, the room will instantly be destroyed
            options.EmptyRoomTtl = 0;

            PhotonNetwork.JoinOrCreateRoom(roomName.text, options, TypedLobby.Default);
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("Created room successfully.");
            roomsCanvases.CurrentRoomCanvas.Show();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("Room creation failed: "+message);
        }
    }
}
