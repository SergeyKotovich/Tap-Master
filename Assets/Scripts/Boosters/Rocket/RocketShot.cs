using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Pool;

public class RocketShot : MonoBehaviour
{
    [SerializeField] private Rockets _rocketsPrefab;

    private SoundsManager _soundsManager;
    private ObjectPool<Rockets> _rocketsPool;

    private void Awake()
    {
        _rocketsPool = new ObjectPool<Rockets>(Create, Get, Release);
    }

    private void Release(Rockets rockets)
    {
        rockets.gameObject.SetActive(false);
    }

    private void Get(Rockets rockets)
    {
        rockets.gameObject.SetActive(true);
        rockets.transform.position = transform.position;
    }

    private Rockets Create()
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
        await UniTask.Delay(7000);
        _rocketsPool.Release(rockets);
    }
}