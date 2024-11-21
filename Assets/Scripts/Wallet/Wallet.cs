using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Wallet : MonoBehaviour 
{
    public event Action<int> AmountMoneyUpdated;
    public event Action MoneyWasNotEnough;
    public int Money { get; private set; }


    private void Awake()
    {
        Money = 1000;
        AmountMoneyUpdated?.Invoke(Money);
    }

    public void AddMoney(int wonMoney)
    {
        Money += wonMoney;
        AmountMoneyUpdated?.Invoke(Money);
    }

    public void SpendMoney(int price)
    {
        Money -= price;
        AmountMoneyUpdated?.Invoke(Money);
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
        AmountMoneyUpdated?.Invoke(Money);
    }
}

