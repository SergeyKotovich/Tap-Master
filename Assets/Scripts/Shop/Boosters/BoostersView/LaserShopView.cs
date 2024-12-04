public class LaserShopView : BoosterShopView
{
    public override void Initialize(ShopPricesConfig shopPricesConfig)
    {
        _costBuyingBooster.text = shopPricesConfig.CostLaser.ToString();
        _costUnlockBooster.text = shopPricesConfig.CostUnlockLaser.ToString();
    }
}