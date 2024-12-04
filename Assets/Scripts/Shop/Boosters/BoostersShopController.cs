using System.Collections.Generic;
using UnityEngine;

public class BoostersShopController : MonoBehaviour
{
    private List<IBooster> _boosters = new();
    private ShopPricesConfig _shopPricesConfig;
    
    public void Initialize(ShopPricesConfig shopPricesConfig)
    {
        _shopPricesConfig = shopPricesConfig;
        _boosters.Add(new BlackHoleBooster(BoostersType.BlackHole, _shopPricesConfig.CostBlackHole));
        _boosters.Add(new LaserBooster(BoostersType.Laser, _shopPricesConfig.CostLaser));
        _boosters.Add(new RocketBooster(BoostersType.Rocket, _shopPricesConfig.CostRockets));
        foreach (var booster in _boosters)
        {
          
        }
    }
}