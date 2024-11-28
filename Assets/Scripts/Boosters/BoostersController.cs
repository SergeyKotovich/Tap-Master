using System;
using UnityEngine;


public class BoostersController : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private BlackHoleController blackHoleController;
    [SerializeField] private LaserShot _laserShot;

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
                if (!blackHoleController.IsActive)
                {
                    blackHoleController.Activate();
                    _inventory.SpendItem(booster.Type);
                }

                break;
            case ItemsType.Laser:
               
                    _laserShot.Shot();
                    _inventory.SpendItem(booster.Type);
                

                break;
        }
    }
}