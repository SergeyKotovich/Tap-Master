using UnityEngine;
using VContainer;

public class UIController : MonoBehaviour
{
    [SerializeField] private ResourceCounterView _resourceCounterView;
    [SerializeField] private WalletView _walletView;
    [SerializeField] private LevelView _levelView;
    [SerializeField] private ShopView _shopView;
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private ShopConfig _shopConfig;
    [SerializeField] private MovesCounterView _movesCounterView;

    [Inject]
    public void Construct(WonMoneyController wonMoneyController, Wallet wallet, IInventoryHandler inventory,
        MovesCounter movesCounter)
    {
        _resourceCounterView.Initialize(wonMoneyController);
        _walletView.Initialize(wallet);
        _shopView.Initialize(_shopConfig);
        _inventoryView.Initialize(inventory, _shopConfig);
        _movesCounterView.Initialize(movesCounter);
    }

    public void UpdateLevelInfo(int levelNumber)
    {
        _levelView.SetLevelNumber(levelNumber);
        _shopView.UpdateCurrentLevel(levelNumber);
        _inventoryView.UpdateInfo(levelNumber);
    }
}