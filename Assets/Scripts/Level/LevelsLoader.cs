using System;
using System.Collections.Generic;
using Cube;
using UniTaskPubSub;
using UnityEngine;
using VContainer;

public class LevelsLoader : MonoBehaviour, ILevelsProvider
{
    public event Action<int> LevelOpened;
    public int Levels => _levels.Count;
    [SerializeField] private List<Level> _levels;
    [SerializeField] private Transform _parentLevel;
    private ObstacleDetector _obstacleDetector;
    private ShakeAnimationController _shakeAnimationController;
    private EffectFactory _effectFactory;
    private AsyncMessageBus _messageBus;
    private int _currentIndexLevel;

    [Inject]
    public void Construct(ObstacleDetector obstacleDetector, ShakeAnimationController shakeAnimationController,
        EffectFactory effectFactory, AsyncMessageBus messageBus)
    {
        _messageBus = messageBus;
        _effectFactory = effectFactory;
        _shakeAnimationController = shakeAnimationController;
        _obstacleDetector = obstacleDetector;
    }

    public void SetLoadData(int currentIndexLevel)
    {
        _currentIndexLevel = currentIndexLevel;
    }
    public Level LoadLevel(int indexLevel)
    {
        
        if (indexLevel >= _levels.Count)
        {
            Debug.Log("Levels is over");
            return null;
        }
        if (indexLevel > _currentIndexLevel)
        {
            _currentIndexLevel = indexLevel;
            LevelOpened?.Invoke(_currentIndexLevel);
        }
        
        var currentLevel = Instantiate(_levels[indexLevel], _parentLevel);
        currentLevel.Initialize(_obstacleDetector, _shakeAnimationController, _effectFactory, _messageBus);
        
        return currentLevel;
    }
}