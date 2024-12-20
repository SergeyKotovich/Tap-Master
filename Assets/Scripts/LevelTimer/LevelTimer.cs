using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniTaskPubSub;
using UnityEngine;

public class LevelTimer : IDisposable
{
    public event Action<float> TimeChanged;

    private bool _isRunning;

    private const int _delay = 100;
    private const float _step = 0.1f;

    private float _currentTime;

    private readonly AsyncMessageBus _messageBus;
    private readonly IDisposable _subscriptions;
    private readonly LevelConfig _levelConfig;

    private CancellationTokenSource _cancellationTokenSource;
    
    public LevelTimer(AsyncMessageBus messageBus, LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
        _messageBus = messageBus;
        _cancellationTokenSource = new CancellationTokenSource();
       _subscriptions = _messageBus.Subscribe<LevelCompleteEvent>(_ => ResetTimer());
    }

    private async UniTaskVoid StartTimer()
    {
        _isRunning = true;

        try
        {
            while (_isRunning)
            {
                await UniTask.Delay(_delay, cancellationToken: _cancellationTokenSource.Token);

                _currentTime -= _step;
                TimeChanged?.Invoke(_currentTime);

                if (_currentTime <= 0)
                {
                    _isRunning = false;
                    _currentTime = 0;
                    TimeChanged?.Invoke(_currentTime);
                    _messageBus.Publish(new LevelFailedEvent());
                }
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Timer was canceled.");
        }
    }

    public void EnableTimer(int countCubes)
    {
        ResetTimer();

        _currentTime = countCubes * _levelConfig.TimeMultiplier;

        StartTimer().Forget();
    }

    private void ResetTimer()
    {
        if (_isRunning)
        {
            _isRunning = false;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }
    }

    public void Dispose()
    {
        _subscriptions.Dispose();
    }
}