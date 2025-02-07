using System;
using JetBrains.Annotations;
using UniTaskPubSub;
using UnityEngine;
using VContainer;

public class GameController : MonoBehaviour
{
    private GameSaveManager _gameSaveManager;
    private IMoneyRestorer _wallet;
    private ShopController _shopController;
    private LevelsLoader _levelsLoader;
    private UIController _uiController;
    private ScoreController _scoreController;
    private ManagerAvailableLevels _managerAvailableLevels;

    private int _indexCurrentLevel;

    private Level _currentLevel;

    private LevelTimer _levelTimer;
    private MovesCounter _movesCounter;

    private GameMod _gameMod;

    private IDisposable _subscription;


    [Inject]
    public void Construct(MovesCounter movesCounter, LevelTimer levelTimer, ScoreController scoreController,
        GameSaveManager gameSaveManager, IMoneyRestorer wallet, ShopController shopController,
        LevelsLoader levelsLoader, UIController uiController, AsyncMessageBus messageBus,
        ManagerAvailableLevels managerAvailableLevels)
    {
        _managerAvailableLevels = managerAvailableLevels;
        _subscription = messageBus.Subscribe<LevelSelectedEvent>(OnLevelSelected);
        _shopController = shopController;
        _wallet = wallet;
        _gameSaveManager = gameSaveManager;
        _scoreController = scoreController;
        _levelTimer = levelTimer;
        _movesCounter = movesCounter;
        _levelsLoader = levelsLoader;
        _uiController = uiController;
    }

    private void OnLevelSelected(LevelSelectedEvent data)
    {
        _indexCurrentLevel = data.IndexLevel;
        LoadLevel();
    }

    public void SetGameMod(int gameMod)
    {
        if (Enum.IsDefined(typeof(GameMod), gameMod))
        {
            _gameMod = (GameMod)gameMod;
        }

        _scoreController.SetGameSettings(gameMod, 0);
        LoadLevel();
    }

    public void SetGameSettings(GameData gameData)
    {
        _gameMod = gameData.GameMod;
        _scoreController.SetGameSettings((int)_gameMod, gameData.Score);
        _indexCurrentLevel = gameData.Level;
        _levelsLoader.SetLoadData(_indexCurrentLevel);
        _wallet.Initialize(gameData.Money);
        _shopController.LoadSaveData(gameData);
        _managerAvailableLevels.LoadSaveData(gameData.IndexesAvailableLevels);
        LoadLevel();
    }

    public void LoadNextLevel()
    {
        _indexCurrentLevel++;
        LoadLevel();
    }

    [UsedImplicitly]
    public void RestartLevel()
    {
        LoadLevel();
    }
                   
    private void LoadLevel()
    {
        if (_currentLevel != null)
        {
            _currentLevel.DestroyLevel();
        }

        _currentLevel = _levelsLoader.LoadLevel(_indexCurrentLevel);
        _gameSaveManager.Save(new GameData(_indexCurrentLevel, _scoreController.CurrentCountPoints, _gameMod,
            _wallet.Money, _shopController.GetDataSkins(), _shopController.GetDataBackgrounds(),
            _shopController.GetDataBoosters(), _managerAvailableLevels.GetAvailableLevels()));
        _uiController.UpdateLevelInfo(_currentLevel.LevelNumber);

        if (_gameMod == GameMod.Easy)
        {
            return;
        }

        _movesCounter.SetCountMoves(_currentLevel);
        if (_gameMod == GameMod.Normal)
        {
            return;
        }

        _levelTimer.EnableTimer(_currentLevel.CountCubes);
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}