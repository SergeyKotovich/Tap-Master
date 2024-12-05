public class BlackHoleShopView : BoosterShopView
{
    private ShopConfig _shopConfig;

    public override void Initialize(ShopConfig shopConfig)
    {
        _shopConfig = shopConfig;
        _costBuyingBooster.text = shopConfig.CostBlackHole.ToString();
        _costUnlockBooster.text = shopConfig.CostUnlockBlackHole.ToString();
        _unlockLevel.text = shopConfig.UnlockLevelForBlackHoleBooster.ToString();
    }

    public override void CanUnlockBooster(int currentLevel)
    {
        if (currentLevel==_shopConfig.UnlockLevelForBlackHoleBooster)
        {
            MakeAvailable();
            base.CanUnlockBooster(currentLevel);
        }
    }
}