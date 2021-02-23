using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.MainMenu
{
    public class RoomListing : MonoBehaviour
    {
        [SerializeField] private Text text;

        public void SetRoomInfo(RoomInfo roomInfo)
        {
            text.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
        }

    }
}
