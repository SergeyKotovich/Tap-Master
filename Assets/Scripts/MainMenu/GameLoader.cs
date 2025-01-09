using JetBrains.Annotations;
using UnityEngine;
using VContainer;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _difficultySelectionMenu;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameScreen;
    
    private GameSaveManager _gameSaveManager;
    private GameController _gameController;

    [Inject]
    public void Construct(GameSaveManager gameSaveManager, GameController gameController)
    {
        _gameController = gameController;
        _gameSaveManager = gameSaveManager;
    }

    [UsedImplicitly]
    public void StartGame()
    {
        _startButton.SetActive(false);
        if (_gameSaveManager.SaveExists())
        {
            var gameData = _gameSaveManager.Load();
            _gameController.SetGameSettings(gameData);
            _mainMenu.SetActive(false);
            _gameScreen.SetActive(true);
        }
        else
        {
            _difficultySelectionMenu.SetActive(true);
        }
    }

    public void ResetData()
    {
        _gameSaveManager.ResetData();
    }
}