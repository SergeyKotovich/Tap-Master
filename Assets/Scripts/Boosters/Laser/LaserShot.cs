using System;
using Cysharp.Threading.Tasks;
using UniTaskPubSub;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

public class LaserShot : MonoBehaviour
{
    [SerializeField] private Rigidbody _laserPrefab;
    [SerializeField] private float _shootForce;
    private int _delay = 7000;
    private bool _isActive;

    private ObjectPool<Rigidbody> _laserPool;
    private IDisposable _subscriptions;

    [Inject]
    public void Construct(AsyncMessageBus messageBus)
    {
        _subscriptions = messageBus.Subscribe<LaserTargetPositionSet>(messageData => Shot(messageData.TargetPosition));
    }

    private void Awake()
    {
        _laserPool = new ObjectPool<Rigidbody>(Create, Get, Release);
    }

    private void Release(Rigidbody laser)
    {
        laser.gameObject.SetActive(false);
        laser.transform.position = transform.position;
        laser.transform.rotation = Quaternion.identity;
    }

    private void Get(Rigidbody laser)
    {
        laser.gameObject.SetActive(true);
    }

    private Rigidbody Create()
    {
        return Instantiate(_laserPrefab, transform.position, Quaternion.identity);
    }


    private async UniTask Shot(Vector3 targetPosition)
    {
        var laser = _laserPool.Get();
        var shootDirection = (targetPosition - transform.position).normalized;
        laser.AddForce(shootDirection * _shootForce, ForceMode.Impulse);
        SoundsManager.Instance.PlayShotLaser();
        await UniTask.Delay(_delay);
        _laserPool.Release(laser);
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}