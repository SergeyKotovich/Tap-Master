using UnityEngine;
using VContainer;

public class UIController : MonoBehaviour
{
    [SerializeField] private ResourceCounterView _resourceCounterView;
    [SerializeField] private WalletView _walletView;
    [SerializeField] private LevelView _levelView;
    
    [Inject]
    public void Construct(WonMoneyController wonMoneyController, Wallet wallet)
    {
        _resourceCounterView.Initialize(wonMoneyController);
        _walletView.Initialize(wallet);
    }

    public void UpdateLevelInfo(int levelNumber)
    {
        _levelView.SetLevelNumber(levelNumber);
    }
}