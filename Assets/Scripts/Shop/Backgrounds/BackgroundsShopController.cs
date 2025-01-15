using System.Collections.Generic;
using UnityEngine;

public class BackgroundsShopController : MonoBehaviour
{
    private List<Background> _backgrounds;
    private IMoneyHandler _moneyHandler;

    public void Initialize(ShopConfig shopConfig, IMoneyHandler moneyHandler, List<Background> backgrounds)
    {
        _backgrounds = backgrounds;
        _moneyHandler = moneyHandler;
        foreach (var background in _backgrounds)
        {
            background.Initialize(shopConfig);
        }
    }

    public bool TryBuyBackground(Background background)
    {
        if (_moneyHandler.HasEnoughMoney(background.Cost))
        {
            _moneyHandler.SpendMoney(background.Cost);
            background.MarkAsBought();
            
            return true;
        }

        return false;
    }

    public void LoadSaveData(List<Background> backgrounds)
    {
        _backgrounds = backgrounds;
        foreach (var background in _backgrounds)
        {
            if (background.WasBought)
            {
                background.MarkAsBought();
            }

            if (background.IsActive)
            {
                
            }
        }
    }
}