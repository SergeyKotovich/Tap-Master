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
    private GameMod _gameMod;

    [Inject]
    public void Construct(MovesCounter movesCounter, LevelTimer levelTimer)
    {
        _levelTimer = levelTimer;
        _movesCounter = movesCounter;
    }

    [UsedImplicitly]
    public void SetGameMod(int gameMod)
    {
        if (Enum.IsDefined(typeof(GameMod), gameMod))
        {
            _gameMod = (GameMod)gameMod;
        }

        StartGame();
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

    private void StartGame()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        _currentLevel = _levelsLoader.LoadLevel(_indexCurrentLevel);
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