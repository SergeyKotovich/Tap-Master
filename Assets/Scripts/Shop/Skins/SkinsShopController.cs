using System.Collections.Generic;
using UnityEngine;

public class SkinsShopController : MonoBehaviour
{
    [SerializeField] private Material _material;
    private List<Skin> _skins;
    private IMoneyHandler _moneyHandler;

    public void Initialize(IMoneyHandler moneyHandler, ShopConfig shopConfig, List<Skin> skins)
    {
        _skins = skins;
        _moneyHandler = moneyHandler;
        foreach (var skin in _skins)
        {
            skin.Initialize(shopConfig);
        }
    }

    public void TryUseSkin(Skin skin)
    {
        if (skin.WasBought)
        {
            _material.color = skin.Color;

            MarkAsUsed(skin);
            return;
        }

        if (_moneyHandler.HasEnoughMoney(skin.Cost))
        {
            _moneyHandler.SpendMoney(skin.Cost);
            skin.MarkAsBought();
            _material.color = skin.Color;
            MarkAsUsed(skin);
        }
    }

    private void MarkAsUsed(Skin skin)
    {
        for (var i = 0; i < _skins.Count; i++)
        {
            if (_skins[i] == skin)
            {
                skin.MarkAsActive();
                skin.EnableBackground();
                continue;
            }

            _skins[i].MarkSkinAsInactive();
            _skins[i].DisableBackground();
        }
    }

    public void LoadSaveData(List<Skin> skins)
    {
        _skins = skins;
        foreach (var skin in _skins)
        {
            if (skin.WasBought)
            {
                skin.MarkAsBought();
            }

            if (skin.IsActive)
            {
                MarkAsUsed(skin);
            }
        }
    }
}