using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.MainMenu
{
    public class RoomListing : MonoBehaviour
    {
        [SerializeField] private CreateOrJoinRoomCanvas _createOrJoinRoomCanvas;
        [SerializeField] private Text text;
        public RoomInfo RoomInfo { get; private set;}

        public void SetRoomInfo(RoomInfo roomInfo)
        {
            RoomInfo = roomInfo;
            text.text = roomInfo.Name+ "("+roomInfo.PlayerCount+"/"+roomInfo.MaxPlayers+")";
        }

        public void onClickButton()
        {
            PhotonNetwork.JoinRoom(RoomInfo.Name);
            _createOrJoinRoomCanvas.gameObject.SetActive(false);
        }

    }
}
