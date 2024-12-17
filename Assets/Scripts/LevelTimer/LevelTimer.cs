using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniTaskPubSub;
using UnityEngine;

public class LevelTimer
{
    public event Action<float> TimeChanged;

    private bool _isRunning;

    private const int _delay = 100;
    private const float _step = 0.1f;

    private float _currentTime;

    private readonly AsyncMessageBus _messageBus;

    private CancellationTokenSource _cancellationTokenSource;

    public LevelTimer(AsyncMessageBus messageBus)
    {
        _messageBus = messageBus;
        _cancellationTokenSource = new CancellationTokenSource();
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
                    _messageBus.Publish(new LevelFailed());
                }
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Timer was canceled.");
        }
    }

    public void EnableTimer(float levelTime)
    {
        if (_isRunning)
        {
            ResetTimer();
        }

        _currentTime = levelTime;
        StartTimer().Forget();
    }

    private void ResetTimer()
    {
        _isRunning = false;

        // Отменяем старый токен и создаем новый
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
        _cancellationTokenSource = new CancellationTokenSource();
    }
}