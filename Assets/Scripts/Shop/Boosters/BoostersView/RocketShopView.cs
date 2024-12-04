public class RocketShopView : BoosterShopView
{
    public override void Initialize(ShopPricesConfig shopPricesConfig)
    {
        _costBuyingBooster.text = shopPricesConfig.CostRockets.ToString();
        _costUnlockBooster.text = shopPricesConfig.CostUnlockRockets.ToString();
    }
}