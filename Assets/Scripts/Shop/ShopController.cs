using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;


public class ShopController : MonoBehaviour
{
    [SerializeField] private ShopConfig _shopConfig;
    [SerializeField] private SkinsShopController _skinsShopController;
    [SerializeField] private BoostersShopController _boostersShopController;
    [SerializeField] private BackgroundsShopController _backgroundsShopController;

    private IInventoryHandler _inventory;


    [Inject]
    public void Construct(IMoneyHandler moneyHandler, IInventoryHandler inventory, List<Booster> boosters,
        List<Background> backgrounds)
    {
        _inventory = inventory;
        _skinsShopController.Initialize(moneyHandler, _shopConfig);
        _boostersShopController.Initialize(_shopConfig, moneyHandler, boosters);
        _backgroundsShopController.Initialize(_shopConfig, moneyHandler, backgrounds);
    }

    public void UseSkin(Skin skin)
    {
        _skinsShopController.TryUseSkin(skin);
    }

    [UsedImplicitly]
    public void BuyBooster(Booster booster)
    {
        if (_boostersShopController.TryBuyBooster(booster))
        {
            _inventory.AddBooster(booster);
        }
    }

    [UsedImplicitly]
    public void UseBackground(Background background)
    {
        if (!background.WasBought)
        {
            if (_backgroundsShopController.TryBuyBackground(background))
            {
                _inventory.UseBackground(background);
            }
        }

        if (background.WasBought)
        {
            _inventory.UseBackground(background);
        }

    }
}