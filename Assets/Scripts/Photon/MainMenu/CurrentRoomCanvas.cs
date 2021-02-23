using UnityEngine;

namespace Photon.MainMenu
{
    public class CurrentRoomCanvas : MonoBehaviour
    {
        private RoomsCanvases roomsCanvases;
        
        public void FirstInitialize(RoomsCanvases canvases)
        {
            roomsCanvases = canvases;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);   
        }
        
        
    }
}
