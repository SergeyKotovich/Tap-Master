using System.Collections.Generic;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    [SerializeField] private List<BoosterShopView> _boostersShopView;
    [SerializeField] private SkinsShopView _skinsShopView;
    [SerializeField] private ShopPricesConfig _shopPricesConfig;

    private void Awake()
    {
        foreach (var boosterShopView in _boostersShopView)
        {
            boosterShopView.Initialize(_shopPricesConfig);
        }

        _skinsShopView.Initialize(_shopPricesConfig);
    }
}