using TMPro;
using UnityEngine;


public class InventoryView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _blackHoleCount;
    [SerializeField] private TextMeshProUGUI _laserCount;

    [SerializeField] private Inventory _inventory;

    private void Start()
    {
        UpdateInventoryView();
//        _inventory.ItemsWereChanged += UpdateInventoryView;
    }

    public void UpdateInventoryView()
    {
        UpdateItemCountView(BoostersType.BlackHole, _blackHoleCount);
        UpdateItemCountView(BoostersType.Laser, _laserCount);
    }

    private void UpdateItemCountView(BoostersType boosterType, TextMeshProUGUI countText)
    {
        //       var item = _inventory.GetItemByType(boosterType);

        //      if (item == null || item.Count <= 0)
        //      {
        //          countText.text = " ";
        //          countText.transform.parent.gameObject.SetActive(false);
        //      }
        //      else
        //      {
        //          countText.transform.parent.gameObject.SetActive(true);
        //          countText.text = item.Count.ToString();
        //      }
        //  }
//
        //  private void OnDestroy()
        //  {
        //      _inventory.ItemsWereChanged -= UpdateInventoryView;
        //  }
    }
}