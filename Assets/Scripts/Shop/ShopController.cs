using JetBrains.Annotations;
using UnityEngine;
using VContainer;


public class ShopController : MonoBehaviour
{
    [SerializeField] private ShopConfig _shopConfig;
    [SerializeField] private SkinsShopController _skinsShopController;
    [SerializeField] private BoostersShopController _boostersShopController;
    private IInventoryHandler _inventory;


    [Inject]
    public void Construct(IMoneyHandler moneyHandler, IInventoryHandler inventory)
    {
        _inventory = inventory;
        _skinsShopController.Initialize(moneyHandler,_shopConfig);
        _boostersShopController.Initialize(_shopConfig, moneyHandler, inventory);
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
}