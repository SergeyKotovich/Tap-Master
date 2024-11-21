using System;
using System.Collections.Generic;
using DefaultNamespace.Inventory;
using DefaultNamespace.Items;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private DefaultData _defaultData;
        
        [SerializeField] private Wallet _wallet;
        [SerializeField] private Inventory _inventory;

        private PlayerDataSaver _playerDataSaver;

        private void Awake()
        {
            _defaultData = new DefaultData();
            _playerDataSaver = new PlayerDataSaver();
        }

        public void TryBuy(ItemsType itemsType)
        {
            var item = _inventory.GetItemByType(itemsType);
            
            if (!_wallet.HasEnoughMoney(item.Price)) return;
            
            _wallet.SpendMoney(item.Price);
            _inventory.AddNewItem(item);
        }

    }
}