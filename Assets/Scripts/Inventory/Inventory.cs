using System;
using System.Collections.Generic;
using DefaultNamespace.Items;
using UnityEngine;
using System.Linq;
using DefaultNamespace.Inventory;

namespace DefaultNamespace.Player
{
    public class Inventory : MonoBehaviour
    {
        public event Action ItemsWereChanged;
        public List<IItem> Items { get; private set; } = new List<IItem>();
        
        private void Start()
        {
            Items.Add(new BlackHoleItem());
            Items.Add(new LaserItem());
            //SetDefaultBoosters();
        }


        public void AddNewItem(IItem item)
        {
            item.AddItem();
            ItemsWereChanged?.Invoke();
            Debug.Log("ты купил " + item.Type);
        }
         
        public IItem GetItemByType(ItemsType itemsType)
        {
            foreach (var item in Items)
            {
                if (item.Type == itemsType)
                {
                    return item;
                }
            }
            return null;
        }
       //public int GetBoughtItemsCount(ItemsType type)
       //{
       //    //var count = Items.FirstOrDefault(booster => booster.Type == type).Count;

       //    var item = Items.Find(item => item.Type == type);
       //    Debug.Log("item = " + item);
       //    Debug.Log("item type = " + item.Type);
       //    Debug.Log("item count = " + item.Count);

       //    return item.Count;
       //}

        public void SpendItem(ItemsType type)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Type == type)
                {
                    Items[i].SpendItem();
                    ItemsWereChanged?.Invoke();
                    return;
                }
            }
        }
        
        public void SpendItem(string type)
        {
            if (Enum.TryParse<ItemsType>(type, out var boosterType))
            {
                SpendItem(boosterType);
            }
            else
            {
                Debug.LogError("Failed to parse ItemType from string: " + type);
            }
        }
        

    }
}