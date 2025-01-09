using System;
using Cysharp.Threading.Tasks;
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

    [Inject]
    public void Construct(MovesCounter movesCounter, LevelTimer levelTimer, ScoreController scoreController,
        GameSaveManager gameSaveManager, IMoneyRestorer wallet)
    {
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
        _uiController.UpdateLevelInfo(_currentLevel.LevelNumber);
        _gameSaveManager.Save(new GameData(_indexCurrentLevel, _scoreController.CurrentCountPoints, _gameMod,
            _wallet.Money));

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