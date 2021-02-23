using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.MainMenu
{
    public class RoomListing : MonoBehaviour
    {
        [SerializeField] private Text text;
        public RoomInfo RoomInfo { get; private set;}

        public void SetRoomInfo(RoomInfo roomInfo)
        {
            RoomInfo = roomInfo;
            text.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
        }

        public void onClickButton()
        {
            PhotonNetwork.JoinRoom(RoomInfo.Name);
        }

    }
}
