using System;
using UniTaskPubSub;
using UnityEngine;

public class WonMoneyController : IDisposable
{
    public event Action<int, int> WinningMoneyCalculated;
    
    private readonly LevelConfig _levelConfig;
    private readonly IDisposable _subscriptions;


    public WonMoneyController(AsyncMessageBus messageBus, LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
        _subscriptions = messageBus.Subscribe<LevelCompleteEvent>(CalculateTotalWinnings);
    }

    private void CalculateTotalWinnings(LevelCompleteEvent data)
    {
        var sumMoney = _levelConfig.LevelVictoryReward * data.CubesCount;
        WinningMoneyCalculated?.Invoke(sumMoney, data.CubesCount);
    }
    
    public void Dispose()
    {
        _subscriptions?.Dispose();
    }
}