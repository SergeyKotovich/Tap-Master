using System;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;

public class GameController : MonoBehaviour
{
    [SerializeField] private LevelsLoader _levelsLoader;
    [SerializeField] private UIController _uiController;

    private int _indexCurrentLevel;

    private Level _currentLevel;
    private LevelTimer _levelTimer;
    private MovesCounter _movesCounter;
    private ScoreController _scoreController;

    private GameMod _gameMod;
    private GameSaveManager _gameSaveManager;
    private IMoneyRestorer _wallet;
    private ShopController _shopController;

    [Inject]
    public void Construct(MovesCounter movesCounter, LevelTimer levelTimer, ScoreController scoreController,
        GameSaveManager gameSaveManager, IMoneyRestorer wallet, ShopController shopController)
    {
        _shopController = shopController;
        _wallet = wallet;
        _gameSaveManager = gameSaveManager;
        _scoreController = scoreController;
        _levelTimer = levelTimer;
        _movesCounter = movesCounter;
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
        _wallet.Initialize(gameData.Money);
        _shopController.LoadSaveData(gameData);

        LoadLevel();
    }

    public void LoadNextLevel()
    {
        if (_currentLevel != null)
        {
            _currentLevel.DestroyLevel();
        }

        _indexCurrentLevel++;
        LoadLevel();
    }

    [UsedImplicitly]
    public void RestartLevel()
    {
        if (_currentLevel != null)
        {
            _currentLevel.DestroyLevel();
        }

        LoadLevel();
    }


    private void LoadLevel()
    {
        _currentLevel = _levelsLoader.LoadLevel(_indexCurrentLevel);
        _gameSaveManager.Save(new GameData(_indexCurrentLevel, _scoreController.CurrentCountPoints, _gameMod,
            _wallet.Money, _shopController.GetDataSkins(), _shopController.GetDataBackgrounds(), _shopController.GetDataBoosters()));
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
}