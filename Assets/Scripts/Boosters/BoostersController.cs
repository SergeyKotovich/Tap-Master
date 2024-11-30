using BlackHole;
using UniTaskPubSub;
using UnityEngine;
using VContainer;


public class BoostersController : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    
    [SerializeField] private BlackHoleController _raycastCollector;
    [SerializeField] private LaserShot _laserShot;
    [SerializeField] private RocketShot _rocketShot;
    
    [Inject]
    public void Construct(AsyncMessageBus messageBus)
    {
        _laserShot.Initialize(messageBus);
        
        
    }
}
