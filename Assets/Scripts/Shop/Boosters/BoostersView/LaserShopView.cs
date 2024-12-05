public class LaserShopView : BoosterShopView
{
    private ShopConfig _shopConfig;

    public override void Initialize(ShopConfig shopConfig)
    {
        _shopConfig = shopConfig;
        _costBuyingBooster.text = shopConfig.CostLaser.ToString();
        _costUnlockBooster.text = shopConfig.CostUnlockLaser.ToString();
        _unlockLevel.text = shopConfig.UnlockLevelForLaserBooster.ToString();
    }
    public override void CanUnlockBooster(int currentLevel)
    {
        if (currentLevel==_shopConfig.UnlockLevelForLaserBooster)
        {
            MakeAvailable();
            base.CanUnlockBooster(currentLevel);
        }
    }
}