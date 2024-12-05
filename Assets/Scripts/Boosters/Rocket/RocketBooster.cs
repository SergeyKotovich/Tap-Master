using UnityEngine;

public class RocketBooster : Booster
{
    public bool WasBought { get; private set; }
    public BoostersType Type { get; private set; }
    public int Price { get; private set; }
    public int Count { get; private set; }

    public override void Initialize(ShopConfig shopConfig)
    {
        Type = BoostersType.Rocket;
        Price = shopConfig.CostRockets;
    }

    public RocketBooster(BoostersType type, int cost)
    {
        Type = type;
        Price = cost;
    }

    public override void AddBooster()
    {
        Count++;
    }

    public override void SpendBooster()
    {
        Count--;
    }
}