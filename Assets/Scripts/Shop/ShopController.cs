using System;
using DefaultNamespace.Inventory;
using UnityEngine;


public class ShopController : MonoBehaviour
{
    [SerializeField] private Player _player;

    public void TryBuyItem(string type)
    {
        if (Enum.TryParse<ItemsType>(type, out var boosterType))
        {
            _player.TryBuy(boosterType);
        }
    }
}