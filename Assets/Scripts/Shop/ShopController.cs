using JetBrains.Annotations;
using UnityEngine;
using VContainer;


public class ShopController : MonoBehaviour
{
    [SerializeField] private ShopConfig _shopConfig;
    [SerializeField] private SkinsShopController _skinsShopController;
    [SerializeField] private BoostersShopController _boostersShopController;
    
    
    [Inject]
    public void Construct(IMoneyHandler moneyHandler)
    {
        _skinsShopController.Initialize(moneyHandler,_shopConfig);
        _boostersShopController.Initialize(_shopConfig);
    }

    [UsedImplicitly]
    public void UseSkin(Skin skin)
    {
        _skinsShopController.TryUseSkin(skin);
    }
}