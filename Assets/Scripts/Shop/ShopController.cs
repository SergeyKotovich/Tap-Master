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
    private readonly List<SkinSaveData> _skinsSaveData = new();
    private readonly List<BackgroundSaveData> _backgroundsSaveData = new();
    private readonly List<BoosterSaveData> _boostersSaveData = new();
    private List<Booster> _boosters;
    private List<Skin> _skins;
    private List<Background> _backgrounds;


    [Inject]
    public void Construct(IMoneyHandler moneyHandler, IInventoryHandler inventory, List<Booster> boosters,
        List<Background> backgrounds, List<Skin> skins)
    {
        _boosters = boosters;
        _backgrounds = backgrounds;
        _skins = skins;
        _inventory = inventory;
        _skinsShopController.Initialize(moneyHandler, _shopConfig, skins);
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
                background.MarkAsActive();
            }
        }

        if (background.WasBought)
        {
            _inventory.UseBackground(background);
            background.MarkAsActive();
        }

        for (var i = 0; i < _backgrounds.Count; i++)
        {
            if (_backgrounds[i] != background)
            {
                _backgrounds[i].MarkAsInactive();
            }
        }
    }

    public void LoadSaveData(GameData gameData)
    {
        LoadSkinsData(gameData.Skins);
        LoadBackgroundsData(gameData.Backgrounds);
        LoadBoostersData(gameData.Boosters);
    }

    private void LoadBoostersData(List<BoosterSaveData> gameDataBoosters)
    {
        for (var i = 0; i < gameDataBoosters.Count; i++)
        {
            if (i < _boosters.Count)
            {
                _boosters[i].ApplyData(gameDataBoosters[i]);
                _inventory.LoadSaveData(_boosters[i]);
            }
        }
    }

    private void LoadBackgroundsData(List<BackgroundSaveData> backgroundSaveData)
    {
        for (var i = 0; i < backgroundSaveData.Count; i++)
        {
            if (i < _backgrounds.Count)
            {
                _backgrounds[i].ApplyData(backgroundSaveData[i]);
            }

            if (_backgrounds[i].IsActive)
            {
                _inventory.UseBackground(_backgrounds[i]);
            }

            _backgroundsShopController.LoadSaveData(_backgrounds);
        }
    }

    private void LoadSkinsData(List<SkinSaveData> savedData)
    {
        for (int i = 0; i < savedData.Count; i++)
        {
            if (i < _skins.Count)
            {
                _skins[i].ApplyData(savedData[i]);
            }
        }

        _skinsShopController.LoadSaveData(_skins);
    }

    public List<SkinSaveData> GetDataSkins()
    {
        _skinsSaveData.Clear();
        foreach (var skin in _skins)
        {
            _skinsSaveData.Add(new SkinSaveData(skin));
        }

        return _skinsSaveData;
    }

    public List<BackgroundSaveData> GetDataBackgrounds()
    {
        _backgroundsSaveData.Clear();
        foreach (var background in _backgrounds)
        {
            _backgroundsSaveData.Add(new BackgroundSaveData(background));
        }

        return _backgroundsSaveData;
    }

    public List<BoosterSaveData> GetDataBoosters()
    {
        _boostersSaveData.Clear();
        foreach (var booster in _boosters)
        {
            _boostersSaveData.Add(new BoosterSaveData(booster));
        }

        return _boostersSaveData;
    }
}