using System.Collections.Generic;
using UnityEngine;

public class BoostersShopController : MonoBehaviour
{
    private IMoneyHandler _moneyHandler;
    private List<Booster> _boosters;

    public void Initialize(ShopConfig shopConfig, IMoneyHandler moneyHandler, List<Booster> boosters)
    {
        _boosters = boosters;
        _moneyHandler = moneyHandler;
        
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