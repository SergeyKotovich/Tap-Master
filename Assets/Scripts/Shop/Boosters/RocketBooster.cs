using UnityEngine;

public class RocketBooster : Booster
{
    public override void Initialize(ShopConfig shopConfig)
    {
        Type = BoostersType.Rocket;
        Price = shopConfig.CostRockets;
    }
    
}