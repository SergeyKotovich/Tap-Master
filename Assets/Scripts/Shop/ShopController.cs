using System;
using System.Collections.Generic;
using DefaultNamespace.Inventory;
using DefaultNamespace.Items;
using UnityEngine;

namespace DefaultNamespace.Shop
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;

        public void TryBuyItem(string type)
        {
            if (Enum.TryParse<ItemsType>(type, out var boosterType))
            {
                _player.TryBuy(boosterType);
            }
        }
    }
}