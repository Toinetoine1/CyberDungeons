using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerConnect : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(-4, 0, 0), Quaternion.identity);
    }

    // void Start()
    // {
    //     PhotonNetwork.ConnectUsingSettings();
    //     Debug.Log("1");
    // }
    //
    // public override void OnConnectedToMaster()
    // {
    //     Debug.Log("2");
    //     PhotonNetwork.JoinLobby();
    // }
    //
    // public override void OnJoinedLobby()
    // {
    //     Debug.Log("3");
    //     PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions() {MaxPlayers = 2}, TypedLobby.Default);
    // }
    //
    // public override void OnJoinedRoom()
    // {
    //     PhotonNetwork.Instantiate("Player", new Vector3(-4, 0, 0), Quaternion.identity);
    // }
}
