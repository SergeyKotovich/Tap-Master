using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Pool;

public class RocketShot : MonoBehaviour
{
    [SerializeField] private RocketsPhysicsHandler _rocketsPrefab;

    private int _delay = 7000;
    private ObjectPool<RocketsPhysicsHandler> _rocketsPool;

    private void Awake()
    {
        _rocketsPool = new ObjectPool<RocketsPhysicsHandler>(Create, Get, Release);
    }

    private void Release(RocketsPhysicsHandler rocketsPhysicsHandler)
    {
        rocketsPhysicsHandler.gameObject.SetActive(false);
    }

    private void Get(RocketsPhysicsHandler rocketsPhysicsHandler)
    {
        rocketsPhysicsHandler.gameObject.SetActive(true);
        rocketsPhysicsHandler.transform.position = transform.position;
    }

    private RocketsPhysicsHandler Create()
    {
        return Instantiate(_rocketsPrefab);
    }

    [UsedImplicitly]
    public void ActivateRocket()
    {
        Shot().Forget();
    }

    private async UniTask Shot()
    {
        var rockets = _rocketsPool.Get();
        rockets.LaunchRockets();
        await UniTask.Delay(_delay);
        _rocketsPool.Release(rockets);
    }
}