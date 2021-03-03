using UnityEngine;

namespace Photon.MainMenu
{
    public class CreateOrJoinRoomCanvas : MonoBehaviour
    {
        [SerializeField] private CreateRoomMenu _createRoomMenu;
        [SerializeField] private RoomListingMenu roomListingMenu;


        private RoomsCanvases roomsCanvases;
        
        public void FirstInitialize(RoomsCanvases canvases)
        {
            roomsCanvases = canvases;
            _createRoomMenu.FirstInitialize(canvases);
            roomListingMenu.FirstInitialize(canvases);
        }


        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}
