using System;
using JetBrains.Annotations;
using UniTaskPubSub;
using UnityEngine;
using VContainer;

public class AimController : MonoBehaviour
{
    [SerializeField] private GameObject _aim;
    private IDisposable _subscriptions;

    [Inject]
    public void Construct(AsyncMessageBus messageBus)
    {
        _subscriptions = messageBus.Subscribe<LaserTargetPositionSet>(_ => DisableAim());
    }

    [UsedImplicitly]
    public void EnableAim()
    {
        _aim.SetActive(true);
    }

    private void Update()
    {
        if (_aim.activeSelf)
        {
           _aim.transform.position = Input.mousePosition;
        }
    }

    private void DisableAim()
    {
        _aim.SetActive(false);
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}