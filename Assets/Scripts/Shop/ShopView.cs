using System.Collections.Generic;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    [SerializeField] private List<BoosterShopView> _boostersShopView;
    [SerializeField] private SkinsShopView _skinsShopView;
    [SerializeField] private List<BackgroundShopView> _backgroundsShopView;

    public void Initialize(ShopConfig shopConfig)
    {
        foreach (var boosterShopView in _boostersShopView)
        {
            boosterShopView.Initialize(shopConfig);
        }

        _skinsShopView.Initialize(shopConfig);
        foreach (var backgroundShopView in _backgroundsShopView)
        {
            backgroundShopView.Initialize(shopConfig);
        }
    }
    public void UpdateCurrentLevel(int currentLevel)
    {
        foreach (var boosterShopView in _boostersShopView)
        {
           boosterShopView.CanUnlockBooster(currentLevel); 
        }
    }
}