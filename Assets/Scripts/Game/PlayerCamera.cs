using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExecuteAfterTime(1));
    }
    
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        
        if (gameObject.GetComponent<PhotonView>().IsMine)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
