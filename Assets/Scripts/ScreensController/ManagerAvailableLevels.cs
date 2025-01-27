using System.Collections.Generic;
using UniTaskPubSub;
using UnityEngine;
using VContainer;

public class ManagerAvailableLevels : MonoBehaviour
{
    [SerializeField] private LevelButtonManager _plateLevelPrefab;
    [SerializeField] private Transform _parent;

    private int _allLevels;
    private List<int> _availableLevels = new();
    private readonly List<LevelButtonManager> _levelPlates = new();
    private ILevelsProvider _levelsProvider;
    private AsyncMessageBus _messageBus;

    [Inject]
    public void Construct(ILevelsProvider levelsProvider, AsyncMessageBus messageBus)
    {
        _messageBus = messageBus;
        _levelsProvider = levelsProvider;
        _allLevels = levelsProvider.Levels;
        _levelsProvider.LevelOpened += UpdateAvailableLevels;

        InitializeLevelsScreen();
        UpdateAvailableLevels(0);
    }
    
    public List<int> GetAvailableLevels()
    {
        return _availableLevels;
    }

    public void LoadSaveData(List<int> availableLevels)
    {
        _availableLevels = availableLevels;
        for (var i = 0; i < _availableLevels.Count; i++)
        {
            _levelPlates[i].SwitchLevelAccess(_availableLevels[i]);
        }
    }

    private void UpdateAvailableLevels(int indexLevel)
    {
        _availableLevels.Add(indexLevel);
        _levelPlates[indexLevel].SwitchLevelAccess(indexLevel);
    }

    private void InitializeLevelsScreen()
    {
        for (var indexLevel = 0; indexLevel < _allLevels; indexLevel++)
        {
           var levelPlate = Instantiate(_plateLevelPrefab, _parent);
           levelPlate.Initialize(_messageBus, indexLevel);
           _levelPlates.Add(levelPlate);
        }
    }

    private void OnDestroy()
    {
        if (_levelsProvider!=null)
        {
            _levelsProvider.LevelOpened -= UpdateAvailableLevels;
        }
        
    }
}