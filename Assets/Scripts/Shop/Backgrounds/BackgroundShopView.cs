using System;
using TMPro;
using UnityEngine;

public class BackgroundShopView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private Background _background;
    [SerializeField] private GameObject _price;
    public void Initialize(ShopConfig shopConfig)
    {
        _cost.text = shopConfig.CostStandardBackground.ToString();
        _background.BackgroundWasBought += DisablePrice;
    }

    private void DisablePrice()
    {
        _price.SetActive(false);
    }

    private void OnDestroy()
    {
        _background.BackgroundWasBought -= DisablePrice;
    }
}