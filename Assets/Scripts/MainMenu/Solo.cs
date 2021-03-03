using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = System.Random;

public class Solo : MonoBehaviourPunCallbacks
{
    public void StartSoloGame()
    {
        var rand = new Random();
        PhotonNetwork.JoinOrCreateRoom("SoloRoom"+rand.Next(0, 1000), new RoomOptions() {MaxPlayers = 1}, TypedLobby.Default);
    }
    
    public override void OnJoinedRoom()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel(1);
    }
}