using System;
using UniTaskPubSub;
using UnityEngine;

public class LevelResourceCounter : IDisposable
{
    public event Action<int, int , int> ResourceCountStart;
    
    private readonly LevelConfig _levelConfig;
    private readonly IDisposable _subscriptions;


    public LevelResourceCounter(AsyncMessageBus messageBus, LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
        _subscriptions = messageBus.Subscribe<LevelCompleteEvent>(CalculateTotalWinnings);
    }

    private void CalculateTotalWinnings(LevelCompleteEvent data)
    {
        var sumMoney = _levelConfig.MoneyPerCube * data.CubesCount;
        var sumPoints = data.CubesCount * _levelConfig.PointsPerCube;
        ResourceCountStart?.Invoke(sumMoney, data.CubesCount, sumPoints);
    }
    
    public void Dispose()
    {
        _subscriptions?.Dispose();
    }
}