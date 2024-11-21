using System;
using DefaultNamespace.Inventory;
using UnityEngine;

namespace DefaultNamespace.Items
{
    public class LaserItem : IItem
    {
        public event Action<int, IItem> WasBought;
        [field:SerializeField]
        public ItemsType Type { get; private set; }
        [field:SerializeField]
        public int Price { get; private set; }
        public int Count { get; private set; }

        public LaserItem()
        {
            Type = ItemsType.Laser;
            Count = 3;
            Price = 100;
        }

        public void SpendItem()
        {
            Count--;
        }

        public void AddItem()
        {
            Count++;
        }
        
        public void Activate()
        {
            // activate
        }
    }
}