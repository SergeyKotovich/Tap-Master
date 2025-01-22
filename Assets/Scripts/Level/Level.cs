using System;
using System.Collections.Generic;
using Cube;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniTaskPubSub;
using UnityEngine;

public class Level : MonoBehaviour, ICountCubesProvider
{
    [field: SerializeField] public int LevelNumber { get; private set; }
    [SerializeField] private List<Cube.Cube> _level;

    private readonly List<Vector3> _cubesTargetPositions = new();
    private readonly float _duration = 0.3f;
    private readonly int _startCubePosition = 13;

    public int CountCubes { get; private set; }
    private IDisposable _subscriptions;
    private AsyncMessageBus _messageBus;
    private readonly int _millisecondsDelay = 500;

    public void Initialize(ObstacleDetector obstacleDetector, ShakeAnimationController shakeAnimationController,
        EffectFactory effectFactory, AsyncMessageBus messageBus)
    {
        foreach (var cube in _level)
        {
            cube.Initialize(obstacleDetector, shakeAnimationController, effectFactory, messageBus);
        }
        _messageBus = messageBus;
        _subscriptions = messageBus.Subscribe<CubeWasDestroyedEvent>(_ => UpdateCountCubes());
        CountCubes = _level.Count;

        if (LevelNumber > 10)
        {
            return;
        }

        ScatterCubes();
    }

    private async UniTask UpdateCountCubes()
    {
        if (CountCubes > 0)
        {
            CountCubes--;
        }

        if (CountCubes <= 0)
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

    private void SetStartPositionsCubes()
    {
        for (var i = 0; i < _level.Count; i++)
        {
            if (_level[i] == null)
            {
                continue;
            }
            _level[i].transform.DOLocalMove(_cubesTargetPositions[i], _duration);
        }
    }

    private void ScatterCubes()
    {
        for (var i = 0; i < _level.Count; i++)
        {
            var startPosition = _level[i].transform.localPosition;
            if (i % 2 == 0)
            {
                startPosition.y += _startCubePosition;
            }
            else if (i % 3 == 0)
            {
                startPosition.x += _startCubePosition;
            }
            else if (i % 3 == 1)
            {
                startPosition.x -= _startCubePosition;
            }
            else
            {
                startPosition.y -= _startCubePosition;
            }

            _cubesTargetPositions.Add(_level[i].transform.localPosition);
            _level[i].transform.position = startPosition;
        }

        SetStartPositionsCubes();
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}