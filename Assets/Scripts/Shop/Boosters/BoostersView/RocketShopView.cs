public class RocketShopView : BoosterShopView
{
    private ShopConfig _shopConfig;

    public override void Initialize(ShopConfig shopConfig)
    {
        _shopConfig = shopConfig;
        _costBuyingBooster.text = shopConfig.CostRockets.ToString();
        _costUnlockBooster.text = shopConfig.CostUnlockRockets.ToString();
        _unlockLevel.text = shopConfig.UnlockLevelForRocketBooster.ToString();
    }

    public override void CanUnlockBooster(int currentLevel)
    {
        if (currentLevel >= _shopConfig.UnlockLevelForRocketBooster)
        {
            MakeAvailable();
        }
    }
}