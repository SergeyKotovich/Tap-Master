using System.Collections.Generic;
using UnityEngine;

public class SkinsShopController : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private List<Skin> _skins;
    private IMoneyHandler _moneyHandler;

    public void Initialize(IMoneyHandler moneyHandler, ShopPricesConfig shopPricesConfig)
    {
        _moneyHandler = moneyHandler;
        foreach (var skin in _skins)
        {
            skin.Initialize(shopPricesConfig);
        }
    }
    
    public void TryUseSkin(Skin skin)
    {
        if (skin.WasBought)
        {
            _material.color = skin.Color;
            
            SwitchBackground(skin);
            return;
        }
       
        if (_moneyHandler.HasEnoughMoney(skin.Cost))
        {
            _moneyHandler.SpendMoney(skin.Cost);
            skin.MarkAsBought();
            _material.color = skin.Color;
            SwitchBackground(skin);
        }

    }

    private void SwitchBackground(Skin skin)
    {
        for (var i = 0; i < _skins.Count; i++)
        {
            if (_skins[i] == skin)
            {
                skin.EnableBackground();
                continue;
            }

            _skins[i].DisableBackground();
        }
    }
}