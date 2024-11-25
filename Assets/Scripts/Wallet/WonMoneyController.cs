using System;
using Cube;
using UniTaskPubSub;
using UnityEngine;

public class WonMoneyController : IDisposable
{
    private readonly LevelConfig _levelConfig;
    public event Action<int> WinningMoneyCalculated;
    private readonly IDisposable _subscriptions;


    public WonMoneyController(AsyncMessageBus messageBus, LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
        _subscriptions = messageBus.Subscribe<LevelCompleteEvent>(CalculateTotalWinnings);
    }

    private void CalculateTotalWinnings(LevelCompleteEvent data)
    {
        var sumMoney = _levelConfig.LevelVictoryReward * data.CubesCount;
        WinningMoneyCalculated?.Invoke(sumMoney);
    }
    
    public void Dispose()
    {
        _subscriptions?.Dispose();
    }
}