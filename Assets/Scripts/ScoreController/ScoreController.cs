using System;
using UniTaskPubSub;

public class ScoreController : IDisposable
{
    public event Action<int, int> CountPointsUpdated;
    private readonly LevelConfig _levelConfig;
    private readonly IDisposable _subscriptions;
    private int _currentCountPoints;

    public ScoreController(LevelConfig levelConfig, AsyncMessageBus messageBus)
    {
        _levelConfig = levelConfig;
        _subscriptions = messageBus.Subscribe<LevelCompleteEvent>(UpdateCountPoints);
    }

    private void UpdateCountPoints(LevelCompleteEvent data)
    {
        var currentPoints = _currentCountPoints;
        _currentCountPoints += data.CubesCount * _levelConfig.PointsPerCube;
        var newCountPoints = _currentCountPoints;
        CountPointsUpdated?.Invoke(currentPoints, newCountPoints);
    }

    public void Dispose()
    {
        _subscriptions?.Dispose();
    }
}