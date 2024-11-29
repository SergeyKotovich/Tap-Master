using System;
using UniTaskPubSub;
using UnityEngine;
using VContainer;


public class BoostersController : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
   // [SerializeField] private BlackHoleController _raycastCollector;
    [SerializeField] private LaserShot _laserShot;

    [Inject]
    public void Construct(AsyncMessageBus messageBus)
    {
        _laserShot.Initialize(messageBus);
    }

    public void TryActivateBooster(string type)
    {
        if (Enum.TryParse<ItemsType>(type, out var boosterType))
        {
            var item = _inventory.GetItemByType(boosterType);
            if (item.Count <= 0) return;

            ActivateBooster(item);
        }
    }

    private void ActivateBooster(IItem booster)
    {
        switch (booster.Type)
        {
            case ItemsType.BlackHole:
                _inventory.SpendItem(booster.Type);
                break;
            case ItemsType.Laser:
                _inventory.SpendItem(booster.Type);
                break;
        }
    }
}