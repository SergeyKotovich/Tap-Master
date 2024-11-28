using System;
using UnityEngine;


public class InventoryController : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private BlackHoleController blackHoleController;

    public void TryActivateBooster(string type)
    {
        if (Enum.TryParse<ItemsType>(type, out var boosterType))
        {
            var item = _inventory.GetItemByType(boosterType);

            if (item.Count <= 0) return;

            if (blackHoleController.IsActive) return;
            blackHoleController.Activate();
            _inventory.SpendItem(boosterType);
        }
    }
}