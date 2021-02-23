using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Photon
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        private string gameVersion = "1";
        
        private void Start()
        {
            Debug.Log("Connecting to server...");
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
