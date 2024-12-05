using System.Collections.Generic;
using UnityEngine;

public class BoostersShopController : MonoBehaviour
{
    private readonly List<IBooster> _boosters = new();
    private ShopConfig _shopConfig;

    public void Initialize(ShopConfig shopConfig)
    {
        _shopConfig = shopConfig;
        
        _boosters.Add(new BlackHoleBooster(BoostersType.BlackHole, _shopConfig.CostBlackHole));
        _boosters.Add(new LaserBooster(BoostersType.Laser, _shopConfig.CostLaser));
        _boosters.Add(new RocketBooster(BoostersType.Rocket, _shopConfig.CostRockets));
        
        foreach (var booster in _boosters)
        {
          
        }
    }
}