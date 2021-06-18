using System;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Solo : MonoBehaviourPunCallbacks
{
    public Text text;
    
    public void StartSoloGame()
    {
        if (!NetworkConnection.connected)
        {
            text.enabled = true;
            return;
        }
        
        var rand = new Random();
        PhotonNetwork.JoinOrCreateRoom("SoloRoom"+rand.Next(0, 1000), new RoomOptions() {MaxPlayers = 1}, TypedLobby.Default);
    }
    
    public override void OnJoinedRoom()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel(2);
    }
}