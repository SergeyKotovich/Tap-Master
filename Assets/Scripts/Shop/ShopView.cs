using System.Collections.Generic;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    [SerializeField] private List<BoosterShopView> _boostersShopView;
    [SerializeField] private SkinsShopView _skinsShopView;
    [SerializeField] private ShopConfig _shopConfig;

    public void Initialize()
    {
        foreach (var boosterShopView in _boostersShopView)
        {
            boosterShopView.Initialize(_shopConfig);
        }

        _skinsShopView.Initialize(_shopConfig);
    }
    public void UpdateCurrentLevel(int currentLevel)
    {
        foreach (var boosterShopView in _boostersShopView)
        {
           boosterShopView.CanUnlockBooster(currentLevel); 
        }
    }
}