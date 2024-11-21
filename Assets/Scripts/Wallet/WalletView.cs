using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI _moneyAmount;
    private float _startAmountMoney ;
    private float _timeUpdateResources = 1f;
    private int _wonMoney;
    
    public void SetAmountMoney(int money)
    {
        _wonMoney = money;
        UpdateAmountMoney();
       
    }
    public void UpdateAmountMoney()
    {
        StartCoroutine(ResourceCounterUtility.CountResources(_moneyAmount, _timeUpdateResources, _startAmountMoney,
            _wonMoney));
        _startAmountMoney = _wonMoney;
    }
}