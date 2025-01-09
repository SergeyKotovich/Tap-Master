using System;
using UniTaskPubSub;
using UnityEngine;

public class LevelResourceCounter : IDisposable , IDifficulty
{
    public event Action<int, int , int> ResourceCountStart;
    
    private readonly LevelConfig _levelConfig;
    private readonly IDisposable _subscriptions;
    private int _difficulty;


    public LevelResourceCounter(AsyncMessageBus messageBus, LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
        _subscriptions = messageBus.Subscribe<LevelCompleteEvent>(CalculateTotalWinnings);
    }

    public void SetDifficulty(int difficulty)
    {
        _difficulty = difficulty;
    }

    private void CalculateTotalWinnings(LevelCompleteEvent data)
    {
        var sumMoney = _levelConfig.MoneyPerCube * data.CubesCount;
        var sumPoints = data.CubesCount * _levelConfig.PointsPerCube * _difficulty;
        ResourceCountStart?.Invoke(sumMoney, data.CubesCount, sumPoints);
    }
    
    public void Dispose()
    {
        _subscriptions?.Dispose();
    }
}