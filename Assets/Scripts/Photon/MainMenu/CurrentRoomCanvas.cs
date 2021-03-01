using UnityEngine;

namespace Photon.MainMenu
{
    public class CurrentRoomCanvas : MonoBehaviour
    {
        [SerializeField] private PlayerListingsMenu _playerListingsMenu;
        [SerializeField] private LeaveRoomMenu _leaveRoomMenu;
        
        private RoomsCanvases roomsCanvases;
        
        public void FirstInitialize(RoomsCanvases canvases)
        {
            roomsCanvases = canvases;
            _playerListingsMenu.FirstInitialize(canvases);
            _leaveRoomMenu.FirstInitialize(canvases);
        }

        public void Show()
        {
            _playerListingsMenu.GetCurrentRoomPlayers();
            roomsCanvases.CreateOrJoinRoomCanvas.gameObject.SetActive(false);
            gameObject.SetActive(true);
            Debug.Log("bonsoir a tous !");
            
        }

        public void Hide()
        {
            gameObject.SetActive(false);   
        }
        
        
    }
}
