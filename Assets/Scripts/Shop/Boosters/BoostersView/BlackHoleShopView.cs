public class BlackHoleShopView : BoosterShopView
{
    public override void Initialize(ShopPricesConfig shopPricesConfig)
    {
        _costBuyingBooster.text = shopPricesConfig.CostBlackHole.ToString();
        _costUnlockBooster.text = shopPricesConfig.CostUnlockBlackHole.ToString();
    }
}