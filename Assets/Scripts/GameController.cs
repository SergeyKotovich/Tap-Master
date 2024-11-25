using UnityEngine;


public class GameController : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private LevelsSwitcher _levelsSwitcher;
    [SerializeField] private LevelsLoader _levelsLoader;
    private int _currentLevel;
    
    public void StartGame()
    {
        _levelsLoader.LoadLevel(_currentLevel);
        _currentLevel++;
    }

    public void LoadNextLevel()
    {
        _levelsLoader.LoadLevel(_currentLevel);
        _currentLevel++;
    }

    public void RemoveAllData()
    {
       // _levelsSwitcher.StartFromBeginning();
       // _wallet.SetDefaultMoney();
    }
}