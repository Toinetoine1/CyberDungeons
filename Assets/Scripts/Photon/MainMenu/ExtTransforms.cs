using UnityEngine;

namespace Photon.MainMenu
{
    public static class ExtTransforms
    {

        public static void DestroyChildren(this Transform t, bool destroyImmediately = false)
        {
            foreach (Transform transform in t)
            {
                if(destroyImmediately)
                    MonoBehaviour.DestroyImmediate(transform.gameObject);
                else
                    MonoBehaviour.Destroy(transform.gameObject);
            }
        }
        
    
    }
}
