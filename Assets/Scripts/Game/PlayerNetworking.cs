using Photon.Pun;
using UnityEngine;

namespace Game
{
    public class PlayerNetworking : MonoBehaviour
    {
        public MonoBehaviour[] scriptsToIgnore;

        private PhotonView photonView;

        void Start()
        {
            photonView = GetComponent<PhotonView>();

            if (!photonView.IsMine)
            {
                foreach (MonoBehaviour behaviour in scriptsToIgnore)
                {
                    behaviour.enabled = false;
                }
            }
        }
    }
}
