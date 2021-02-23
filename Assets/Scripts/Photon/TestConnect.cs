using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Photon
{
    public class TestConnect : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            Debug.Log("Connecting to server...");
            PhotonNetwork.GameVersion = "0.0.1";
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to server !");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("Deconnecting: "+cause);
        }
    }
}
