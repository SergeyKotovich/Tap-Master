using System.Collections.Generic;
using Cube;
using UnityEngine;
using VContainer;


public class LevelsController : MonoBehaviour
{
    [SerializeField] private LevelsLoader _levelsLoader;
    [SerializeField] private List<Level> _levels;

    [Inject]
    public void Construct(ObstacleDetector obstacleDetector, ShakeAnimationController shakeAnimationController,
        EffectFactory effectFactory)
    {
        _levelsLoader.Initialize(_levels);

        foreach (var level in _levels)
        {
            level.Initialize(obstacleDetector, shakeAnimationController, effectFactory);
        }
    }

    public void LoadGame(int indexLevel)
    {
        _levelsLoader.LoadLevel(indexLevel);
    }
}