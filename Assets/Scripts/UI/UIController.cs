using UnityEngine;
using VContainer;

public class UIController : MonoBehaviour
{
    [SerializeField] private LevelResourceCounterView _levelResourceCounterView;
    [SerializeField] private WalletView _walletView;
    [SerializeField] private LevelView _levelView;
    [SerializeField] private ShopView _shopView;
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private ShopConfig _shopConfig;
    [SerializeField] private MovesCounterView _movesCounterView;
    [SerializeField] private ScoreView _scoreView;

    [Inject]
    public void Construct(LevelResourceCounter levelResourceCounter, Wallet wallet, IInventoryHandler inventory,
        MovesCounter movesCounter, ScoreController scoreController)
    {
        _levelResourceCounterView.Initialize(levelResourceCounter);
        _walletView.Initialize(wallet);
        _shopView.Initialize(_shopConfig);
        _inventoryView.Initialize(inventory, _shopConfig);
        _movesCounterView.Initialize(movesCounter);
        _scoreView.Initialize(scoreController);
    }

    public void UpdateLevelInfo(int levelNumber)
    {
        _levelView.SetLevelNumber(levelNumber);
        _shopView.UpdateCurrentLevel(levelNumber);
        _inventoryView.UpdateInfo(levelNumber);
    }
}