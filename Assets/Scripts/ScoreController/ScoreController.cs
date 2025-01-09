using System;
using UniTaskPubSub;

public class ScoreController : IDisposable
{
    public event Action<int, int> CountPointsUpdated;
    private readonly LevelConfig _levelConfig;
    private readonly IDisposable _subscriptions;
    public int CurrentCountPoints { get; private set; }
    private int _difficulty;
    
    private readonly IDifficulty _levelResourceCounter;

    public ScoreController(LevelConfig levelConfig, AsyncMessageBus messageBus, IDifficulty levelResourceCounter)
    {
        _levelResourceCounter = levelResourceCounter;
        _levelConfig = levelConfig;
        _subscriptions = messageBus.Subscribe<LevelCompleteEvent>(UpdateCountPoints);
    }

    public void SetGameSettings(int difficulty, int points)
    {
        _difficulty = difficulty;
        CurrentCountPoints = points;
        _levelResourceCounter.SetDifficulty(difficulty);
        CountPointsUpdated?.Invoke(CurrentCountPoints,CurrentCountPoints);
    }
    
    private void UpdateCountPoints(LevelCompleteEvent data)
    {
        var currentPoints = CurrentCountPoints;
        CurrentCountPoints += data.CubesCount * _levelConfig.PointsPerCube * _difficulty;
        var newCountPoints = CurrentCountPoints;
        CountPointsUpdated?.Invoke(currentPoints, newCountPoints);
    }

    public void Dispose()
    {
        _subscriptions?.Dispose();
    }
}