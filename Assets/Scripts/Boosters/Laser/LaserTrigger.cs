using Cube;
using UnityEngine;

public class LaserTrigger : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GlobalConstants.CUBE_TAG))
        {
            other.GetComponent<ICubeDestroyer>().DestroyCube();
            SoundsManager.Instance.PlayLaserHit();
        }
    }
    
}