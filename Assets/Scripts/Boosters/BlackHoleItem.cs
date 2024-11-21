using System;
using DefaultNamespace.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Items
{
    public class BlackHoleItem :  IItem
    {
        public event Action<int, IItem> WasBought;
        [field:SerializeField]
        public ItemsType Type { get; private set; }
        [field:SerializeField]
        public int Price { get; private set; }
        public int Count { get; private set; }
        
        public BlackHoleItem()
        {
            Type = ItemsType.BlackHole;
            Count = 3;
            Price = 200;
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