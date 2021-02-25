using UnityEngine;

namespace Photon.MainMenu
{
    public class CurrentRoomCanvas : MonoBehaviour
    {
        [SerializeField] private PlayerListingsMenu _playerListingsMenu;
        private RoomsCanvases roomsCanvases;
        
        public void FirstInitialize(RoomsCanvases canvases)
        {
            roomsCanvases = canvases;
        }

        public void Show()
        {
            _playerListingsMenu.GetCurrentRoomPlayers();
            roomsCanvases.CreateOrJoinRoomCanvas.gameObject.SetActive(false);
            gameObject.SetActive(true);
            Debug.Log("bonsoir a tous !");
            
        }

        private void Hide()
        {
            gameObject.SetActive(false);   
        }
        
        
    }
}
