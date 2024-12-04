using JetBrains.Annotations;
using UnityEngine;
using VContainer;


public class ShopController : MonoBehaviour
{
    [SerializeField] private ShopPricesConfig _shopPricesConfig;
    [SerializeField] private SkinsShopController _skinsShopController;
    [SerializeField] private BoostersShopController _boostersShopController;
    
    [Inject]
    public void Construct(IMoneyHandler moneyHandler)
    {
        _skinsShopController.Initialize(moneyHandler,_shopPricesConfig);
        _boostersShopController.Initialize(_shopPricesConfig);
    }

    [UsedImplicitly]
    public void UseSkin(Skin skin)
    {
        _skinsShopController.TryUseSkin(skin);
    }
    

    
}