using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerConnect : MonoBehaviourPunCallbacks
{
    [SerializeField] 
    public GameObject PlayerPrefab;
 
    private void Start()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        pool.ResourceCache.Add(PlayerPrefab.name, PlayerPrefab);

        if (PhotonNetwork.IsMasterClient)
        {
            foreach (Player pl in PhotonNetwork.CurrentRoom.Players.Values)
            {
                GameObject obj = PhotonNetwork.Instantiate("Player", new Vector3(-4, 0, 0), Quaternion.identity);
                obj.name = pl.NickName;
                obj.GetComponent<PhotonView>().TransferOwnership(pl);
            }   
        }
    }
    
    // void Start()
    // {
    //     PhotonNetwork.ConnectUsingSettings();
    //     DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
    //     pool.ResourceCache.Add(PlayerPrefab.name, PlayerPrefab);
    //     
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
    //     GameObject obj = PhotonNetwork.Instantiate("Player", new Vector3(-4, 0, 0), Quaternion.identity);
    //     obj.name = "1";
    //     if (PhotonNetwork.IsMasterClient)
    //     {
    //         Debug.Log("set player1");
    //         Player1 = obj;
    //     }
    //     else
    //     {
    //         Debug.Log("set player2");
    //         Player2 = obj;
    //     }
    // }
}
