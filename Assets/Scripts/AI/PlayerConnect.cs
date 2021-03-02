using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerConnect : MonoBehaviourPunCallbacks
{
    // private void Start()
    // {
    //     PhotonNetwork.Instantiate("Prefabs/Characters/Player", new Vector3(-4, 0, 0), Quaternion.identity);
    // }

    [SerializeField] 
    public GameObject PlayerPrefab;

    public GameObject Player1;
    public GameObject Player2;
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        pool.ResourceCache.Add(PlayerPrefab.name, PlayerPrefab);
        
        Debug.Log("1");
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("2");
        PhotonNetwork.JoinLobby();
    }
    
    public override void OnJoinedLobby()
    {
        Debug.Log("3");
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions() {MaxPlayers = 2}, TypedLobby.Default);
    }
    
    public override void OnJoinedRoom()
    {
        GameObject obj = PhotonNetwork.Instantiate("Player", new Vector3(-4, 0, 0), Quaternion.identity);
        if (PhotonNetwork.IsMasterClient)
            Player1 = obj;
        else
            Player2 = obj;
    }
}
