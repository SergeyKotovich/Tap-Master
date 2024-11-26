using System;
using JetBrains.Annotations;
using UniTaskPubSub;
using UnityEngine;
using VContainer;

public class GameController : MonoBehaviour
{
    [SerializeField] private LevelsLoader _levelsLoader;
    [SerializeField] private UIController _uiController;
    private int _indexCurrentLevel;
    private Level _currentLevel;

    public void StartGame()
    {
        _currentLevel = _levelsLoader.LoadLevel(_indexCurrentLevel);
        _uiController.UpdateLevelInfo(_currentLevel.LevelNumber);
    }

    public void LoadNextLevel()
    {
        if (_currentLevel != null)
        {
            _currentLevel.DestroyLevel();
        }
        _indexCurrentLevel++;
        _currentLevel = _levelsLoader.LoadLevel(_indexCurrentLevel);
        _uiController.UpdateLevelInfo(_currentLevel.LevelNumber);
    }

    [UsedImplicitly]
    public void RestartLevel()
    {
        if (_currentLevel != null)
        {
            _currentLevel.DestroyLevel();
        }

        _currentLevel = _levelsLoader.LoadLevel(_indexCurrentLevel);
    }
}