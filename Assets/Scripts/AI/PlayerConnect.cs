using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerConnect : MonoBehaviourPunCallbacks
{
    [SerializeField] 
    public GameObject PlayerPrefab;

    public static List<GameObject> players;
 
    private void Start()
    {
        players = new List<GameObject>();
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        pool.ResourceCache.Add(PlayerPrefab.name, PlayerPrefab);

        if (PhotonNetwork.IsMasterClient)
        {
            foreach (Player pl in PhotonNetwork.CurrentRoom.Players.Values)
            {
                GameObject obj = PhotonNetwork.Instantiate("Player", new Vector3(-4, 0, 0), Quaternion.identity);
                obj.name = pl.NickName;
                obj.GetComponent<PhotonView>().TransferOwnership(pl);
                players.Add(obj);
            }
        }

        //StartCoroutine(ExecuteAfterTime(1));
    }
    
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
 
        foreach (Player pl in PhotonNetwork.CurrentRoom.Players.Values)
        {
            foreach (Player loopPlayer in PhotonNetwork.CurrentRoom.Players.Values)
            {
                if (pl.Equals(loopPlayer))
                    continue;
                GameObject.Find(loopPlayer.NickName).transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
}
