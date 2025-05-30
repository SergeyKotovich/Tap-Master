using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _difficultySelectionMenu;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _moveCounter;
    [SerializeField] private GameObject _timer;
    
    private GameSaveManager _gameSaveManager;
    private GameController _gameController;
    private ScenesLoader _scenesLoader;

    [Inject]
    public void Construct(GameSaveManager gameSaveManager, GameController gameController, ScenesLoader scenesLoader)
    {
        _scenesLoader = scenesLoader;
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
            switch (gameData.GameMod)
            {
                case GameMod.Easy:
                    _moveCounter.SetActive(false);
                    _timer.SetActive(false);
                    break;
                case GameMod.Normal:
                    _timer.SetActive(false);
                    break;
            }
        }
        else
        {
            _difficultySelectionMenu.SetActive(true);
        }
    }

    public void ResetData()
    {
        _gameSaveManager.ResetData();
        _scenesLoader.ReloadGame();
    }
}