public class BlackHoleBooster : Booster
{
    public override void Initialize(ShopConfig shopConfig)
    {
        Type = BoostersType.BlackHole;
        Price = shopConfig.CostBlackHole;
    }
}

