using Cube;
using UnityEngine;

namespace BlackHole
{
    public class BlackHoleTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GlobalConstants.CUBE_TAG))
            {
                other.GetComponent<ICubeDestroyer>().DestroyCube();
                SoundsManager.Instance.PlayDestroyCubeInHole();
            }
        }
    }
}