using System;
using UnityEngine;
using VContainer;

public class Wallet : IDisposable
{
    public event Action<int, int> AmountMoneyUpdated;
    public event Action MoneyWasNotEnough;
    public int Money { get; private set; }
    private WonMoneyController _wonMoneyController;
    
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
        AmountMoneyUpdated?.Invoke(startMoney,totalMoney);
    }

    public void SpendMoney(int price)
    {
        Money -= price;
      //  AmountMoneyUpdated?.Invoke(Money);
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

    public void SetDefaultMoney()
    {
        Money = 0;
    //    AmountMoneyUpdated?.Invoke(Money);
    }
    
    public void Dispose()
    {
        _wonMoneyController.WinningMoneyCalculated -= AddMoney;
    }
}

