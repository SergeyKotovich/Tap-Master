using System.Collections.Generic;
using UnityEngine;

public class BoostersShopController : MonoBehaviour
{
    [SerializeField] private List<Booster> _boosters;

    private IMoneyHandler _moneyHandler;

    public void Initialize(ShopConfig shopConfig, IMoneyHandler moneyHandler, IInventoryHandler inventory)
    {
        _moneyHandler = moneyHandler;
        
        inventory.Initialize(_boosters);

        foreach (var booster in _boosters)
        {
            booster.Initialize(shopConfig);
        }
    }

    public bool TryBuyBooster(Booster booster)
    {
        if (_moneyHandler.HasEnoughMoney(booster.Price))
        {
            _moneyHandler.SpendMoney(booster.Price);
            return true;
        }

        return false;
    }
}