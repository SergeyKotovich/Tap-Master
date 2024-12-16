using JetBrains.Annotations;
using UnityEngine;
using VContainer;

public class GameController : MonoBehaviour
{
    [SerializeField] private LevelsLoader _levelsLoader;
    [SerializeField] private UIController _uiController;

    private MovesCounter _movesCounter;

    private int _indexCurrentLevel;
    private Level _currentLevel;

    [Inject]
    public void Construct(MovesCounter movesCounter)
    {
        _movesCounter = movesCounter;
    }

    public void StartGame()
    {
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
        _movesCounter.SetCountMoves(_currentLevel);
    }
}