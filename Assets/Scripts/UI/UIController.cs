using UnityEngine;
using VContainer;

public class UIController : MonoBehaviour
{
    [SerializeField] private ResourceCounterView _resourceCounterView;
    [SerializeField] private WalletView _walletView;
    [SerializeField] private LevelView _levelView;
    [SerializeField] private ShopView _shopView;
    [SerializeField] private InventoryView _inventoryView;
    
    [Inject]
    public void Construct(WonMoneyController wonMoneyController, Wallet wallet , IInventoryHandler inventory)
    {
        _resourceCounterView.Initialize(wonMoneyController);
        _walletView.Initialize(wallet);
        _shopView.Initialize();
        _inventoryView.Initialize(inventory);
    }

    public void UpdateLevelInfo(int levelNumber)
    {
        _levelView.SetLevelNumber(levelNumber);
        _shopView.UpdateCurrentLevel(levelNumber);
    }
}