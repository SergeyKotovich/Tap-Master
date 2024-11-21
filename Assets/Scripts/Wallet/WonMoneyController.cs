using System;
using Cube;
using UnityEngine;

public class WonMoneyController : MonoBehaviour 
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private CubesController _cubesController;
    public event Action <int> WinningMoneyCalculated;

    public void CalculateWonAmountMoney()
    {
        var sumMoney = _levelConfig.LevelVictoryReward * _cubesController.CountCubsInTotal;
        WinningMoneyCalculated?.Invoke(sumMoney);
    }
}
