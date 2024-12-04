using System;
using System.Collections.Generic;
using Cube;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniTaskPubSub;
using UnityEngine;

public class Level : MonoBehaviour
{
    [field: SerializeField] public int LevelNumber { get; private set; }
    [SerializeField] private List<Cube.Cube> _level;
    [SerializeField] private LevelSpawnerConfig _levelSpawnerConfig;

    private readonly List<Vector3> _cubesTargetPositions = new();
    private readonly float _duration = 0.3f;

    private int _countCubes;
    private IDisposable _subscriptions;
    private AsyncMessageBus _messageBus;
    private readonly int _millisecondsDelay = 1000;

    public void Initialize(ObstacleDetector obstacleDetector, ShakeAnimationController shakeAnimationController,
        EffectFactory effectFactory, AsyncMessageBus messageBus)
    {
        foreach (var cube in _level)
        {
            cube.Initialize(obstacleDetector, shakeAnimationController, effectFactory, messageBus);
        }

        _messageBus = messageBus;
        _subscriptions = messageBus.Subscribe<CubeWasDestroyedEvent>(_ => UpdateCountCubes());
        _countCubes = _level.Count;
        
        if (LevelNumber > 6)
        {
            return;
        }

        ScatterCubes();
    }

    private async UniTask UpdateCountCubes()
    {
        if (_countCubes > 0)
        {
            _countCubes--;
        }

        if (_countCubes <= 0)
        {
            await UniTask.Delay(_millisecondsDelay);
            var countCubesInLevel = _level.Count;
            _messageBus.Publish(new LevelCompleteEvent(countCubesInLevel));
        }
    }

    public void DestroyLevel()
    {
        Destroy(gameObject);
    }

    private async UniTask SetStartPositionsCubes()
    {
        for (int i = 0; i < _level.Count; i++)
        {
            _level[i].transform.DOLocalMove(_cubesTargetPositions[i], _duration);
            await UniTask.NextFrame();
        }
    }

    private void ScatterCubes()
    {
        for (var i = 0; i < _level.Count; i++)
        {
            var startPosition = _level[i].transform.localPosition;
            if (i % 2 == 0)
            {
                startPosition.y += _levelSpawnerConfig.StartCubePosition;
            }
            else if (i % 3 == 0)
            {
                startPosition.x += _levelSpawnerConfig.StartCubePosition;
            }
            else if (i % 3 == 1)
            {
                startPosition.x -= _levelSpawnerConfig.StartCubePosition;
            }
            else
            {
                startPosition.y -= _levelSpawnerConfig.StartCubePosition;
            }

            _cubesTargetPositions.Add(_level[i].transform.localPosition);
            _level[i].transform.position = startPosition;
        }

        SetStartPositionsCubes().Forget();
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}