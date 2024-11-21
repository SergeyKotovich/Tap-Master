using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(SphereCollider))]
public class SingularityCore : MonoBehaviour
{
    //This script is responsible for what happens when the pullable objects reach the core
    //by default, the game objects are simply turned off
    //as this is much more performant than destroying the objects

    [FormerlySerializedAs("_blackHole")] [SerializeField] private BlackHoleController blackHoleController;
    private int _countCubesDestroy;
    private SoundsManager _soundsManager; // Звук
    
    private void Awake()
    {
        if(GetComponent<SphereCollider>()){
            GetComponent<SphereCollider>().isTrigger = true;
        }
        
        _soundsManager = Container.Instance.SoundsManager; // звук
    }
    
    
    void OnTriggerStay (Collider other) {
        if(other.GetComponent<SingularityPullable>())
        {
            
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            Destroy(other.gameObject);
            _countCubesDestroy++;
            Debug.Log(_countCubesDestroy);
            
            blackHoleController.OnCubeWasDestroyed();
            
            _soundsManager.PlayDestroyCubeInHole();
        }
    }

    private void OnEnable()
    {
        _countCubesDestroy = 0;
    }
    
}
