using System;
using UnityEngine;
using VContainer;

public class Wallet : IDisposable, IMoneyHandler
{
    public event Action<int, int> AmountMoneyUpdated;
    public event Action MoneyWasNotEnough;
    public int Money { get; private set; }
    private readonly WonMoneyController _wonMoneyController;

    public Wallet(WonMoneyController wonMoneyController)
    {
        _wonMoneyController = wonMoneyController;
        _wonMoneyController.WinningMoneyCalculated += AddMoney;
    }

    private void AddMoney(int wonMoney, int value)
    {
        var startMoney = Money;
        Money += wonMoney;
        var totalMoney = Money;
        AmountMoneyUpdated?.Invoke(startMoney, totalMoney);
    }

    public void SpendMoney(int price)
    {
        var startMoney = Money;
        Money -= price;
        var totalMoney = Money;
        AmountMoneyUpdated?.Invoke(startMoney,totalMoney);
    }

    public bool HasEnoughMoney(int price)
    {
        if (Money >= price)
        {
            return true;
        }

        Debug.Log("Денег нет!!!");
        MoneyWasNotEnough?.Invoke();
        return false;
    }
    
    public void Dispose()
    {
        _wonMoneyController.WinningMoneyCalculated -= AddMoney;
    }
}