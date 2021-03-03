using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.MainMenu
{
    public class PlayerListing : MonoBehaviour
    {
        [SerializeField] private Text text;

        public Player Player { get; private set; }

        public void SetPlayerInfo(Player player)
        {
            Player = player;
            text.text = player.NickName;
        }

    }
}
