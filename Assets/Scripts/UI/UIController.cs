using UnityEngine;
using VContainer;

public class UIController : MonoBehaviour
{
    [SerializeField] private ResourceCounterView _resourceCounterView;
    [SerializeField] private WalletView _walletView;
    [SerializeField] private LevelView _levelView;
    [SerializeField] private ShopView _shopView;
    
    [Inject]
    public void Construct(WonMoneyController wonMoneyController, Wallet wallet)
    {
        _resourceCounterView.Initialize(wonMoneyController);
        _walletView.Initialize(wallet);
        _shopView.Initialize();
    }

    public void UpdateLevelInfo(int levelNumber)
    {
        _levelView.SetLevelNumber(levelNumber);
        _shopView.UpdateCurrentLevel(levelNumber);
    }
}