using Cube;
using DefaultNamespace.Player;
using UnityEngine;


public class GameController : MonoBehaviour
{
    [SerializeField] private ScreensController.ScreensController _screensController;
    [SerializeField] private CubesController _cubesController;

    [SerializeField] private MouseClickHandler _mouseClickHandler;
    [SerializeField] private MovesCounter _movesCounter;

    [SerializeField] private WonMoneyController _wonMoneyController;
    [SerializeField] private WonMoneyControllerView _wonMoneyControllerView;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private WalletView _walletView;
    [SerializeField] private LevelsSwitcher _levelsSwitcher;
    [SerializeField] private LevelsController _levelsController;
    [SerializeField] private SoundsManager _soundsManager;
    private int _currentLevel;

    private void Awake()
    {
        _cubesController.LastCubeWasGone += _screensController.ShowVictoryScreen;
        _cubesController.LastCubeWasGone += _soundsManager.PlayVictory;
        _screensController.VictoryScreenLoaded += _wonMoneyControllerView.ShowCountingCubes;
        _cubesController.LastCubeWasGone += _wonMoneyController.CalculateWonAmountMoney;
        _wonMoneyController.WinningMoneyCalculated += _wonMoneyControllerView.SetWonAmountMoney;
        _wonMoneyController.WinningMoneyCalculated += _wallet.AddMoney;
        _wallet.AmountMoneyUpdated += _walletView.SetAmountMoney;
        _wallet.AmountMoneyUpdated += _walletView.SetAmountMoney;

        _mouseClickHandler.CubeWasTaped += _movesCounter.SpendOneMove;
        _mouseClickHandler.CubeWasTaped += _soundsManager.PlayClick;
        _movesCounter.AllMovesWasSpent += _screensController.ShowDefeatScreen;
        _movesCounter.AllMovesWasSpent += _soundsManager.PlayDefeat;
        StartGame();
    }

    public void StartGame()
    {
        _levelsController.LoadGame(3);
        _cubesController.Initialize();
    }

    public void RemoveAllData()
    {
        _levelsSwitcher.StartFromBeginning();
        _wallet.SetDefaultMoney();
    }

    private void OnDestroy()
    {
        _cubesController.LastCubeWasGone -= _screensController.ShowVictoryScreen;
        _screensController.VictoryScreenLoaded -= _wonMoneyControllerView.ShowCountingCubes;
        _cubesController.LastCubeWasGone -= _wonMoneyController.CalculateWonAmountMoney;
        _wonMoneyController.WinningMoneyCalculated -= _wonMoneyControllerView.SetWonAmountMoney;
        _wonMoneyController.WinningMoneyCalculated -= _wallet.AddMoney;
        _wallet.AmountMoneyUpdated -= _walletView.SetAmountMoney;

        _mouseClickHandler.CubeWasTaped -= _movesCounter.SpendOneMove;
        _movesCounter.AllMovesWasSpent -= _screensController.ShowDefeatScreen;
    }
}