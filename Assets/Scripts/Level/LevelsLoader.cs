using System.Collections.Generic;
using Cube;
using UniTaskPubSub;
using UnityEngine;
using VContainer;

public class LevelsLoader : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;
    [SerializeField] private Transform _parentLevel;
    private ObstacleDetector _obstacleDetector;
    private ShakeAnimationController _shakeAnimationController;
    private EffectFactory _effectFactory;
    private AsyncMessageBus _messageBus;

    [Inject]
    public void Construct(ObstacleDetector obstacleDetector, ShakeAnimationController shakeAnimationController,
        EffectFactory effectFactory, AsyncMessageBus messageBus)
    {
        _messageBus = messageBus;
        _effectFactory = effectFactory;
        _shakeAnimationController = shakeAnimationController;
        _obstacleDetector = obstacleDetector;
    }


    public void LoadLevel(int indexLevel)
    {
        if (indexLevel >= _levels.Count)
        {
            Debug.Log("Levels is over");
            return;
        }

        var currentLevel = Instantiate(_levels[indexLevel], _parentLevel);
        currentLevel.Initialize(_obstacleDetector, _shakeAnimationController, _effectFactory, _messageBus);
    }
}