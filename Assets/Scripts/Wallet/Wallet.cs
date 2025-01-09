using System;
using UnityEngine;
using VContainer;

public class Wallet : IDisposable, IMoneyHandler , IMoneyRestorer
{
    public event Action<int, int> AmountMoneyUpdated;
    public event Action MoneyWasNotEnough;
    public int Money { get; private set; }
    private readonly LevelResourceCounter _levelResourceCounter;

    public Wallet(LevelResourceCounter levelResourceCounter)
    {
        _levelResourceCounter = levelResourceCounter;
        _levelResourceCounter.ResourceCountStart += AddMoney;
    }

    public void Initialize(int money)
    {
        Money = money;
        AmountMoneyUpdated?.Invoke(Money, Money);
    }

    private void AddMoney(int wonMoney, int value, int points)
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
        _levelResourceCounter.ResourceCountStart -= AddMoney;
    }
}