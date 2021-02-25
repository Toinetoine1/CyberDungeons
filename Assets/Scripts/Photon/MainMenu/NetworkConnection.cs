using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Photon
{
    public class NetworkConnection : MonoBehaviourPunCallbacks
    {
        private string gameVersion = "1";
        
        private void Start()
        {
            Debug.Log("Connecting to server...");
            int ran = Random.Range(0, 999);
            PhotonNetwork.NickName = "Test"+ran;
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to server !");

            PhotonNetwork.JoinLobby();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("Deconnecting: "+cause);
        }
    }
}
