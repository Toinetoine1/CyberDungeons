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
}
