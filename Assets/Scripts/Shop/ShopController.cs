using System.Collections.Generic;
using UnityEngine;


public class ShopController : MonoBehaviour
{
    [SerializeField] private ShopPricesConfig _shopPricesConfig;

    private List<IBooster> _boosters = new();

    private void Awake()
    {
        _boosters.Add(new BlackHoleBooster(BoostersType.BlackHole, _shopPricesConfig.CostBlackHole));
        _boosters.Add(new LaserBooster(BoostersType.Laser, _shopPricesConfig.CostLaser));
        _boosters.Add(new RocketBooster(BoostersType.Rocket, _shopPricesConfig.CostRockets));
    }
}