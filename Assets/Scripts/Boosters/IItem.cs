using System;
using DefaultNamespace.Inventory;
using UnityEngine;

namespace DefaultNamespace.Items
{
    public interface IItem
    {
        public event Action<int, IItem> WasBought;
        public ItemsType Type { get; }
        public int Price { get; }
        public int Count { get; }

        public void SpendItem();
        public void AddItem();

    }
}