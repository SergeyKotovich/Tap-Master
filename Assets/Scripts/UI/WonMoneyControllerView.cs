using System;
using System.Collections;
using Cube;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using VContainer;

public class WonMoneyControllerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyAmountText;
    [SerializeField] private TextMeshProUGUI _cubesAmount;

    private CubesController _cubesController;
    [SerializeField] private float _timeUpdateResources;

    private int _wonMoney;
    private float _startAmountMoney;
    private float _startAmountCubes;
    private WonMoneyController _wonMoneyController;

    [Inject]
    public void Construct(WonMoneyController wonMoneyController)
    {
         _wonMoneyController = wonMoneyController;
  //       _wonMoneyController.WinningMoneyCalculated += ShowCountingResources;
    }
    
    private async void ShowCountingResources(int wonMoney)
    {
        _startAmountMoney = 0;
        _startAmountCubes = 0f;
  //      await UniTask.NextFrame();
 //       ResourceCounterUtility.CountResources(_cubesAmount, _timeUpdateResources,
 //           _startAmountCubes, _cubesController.CountCubsInTotal);
        await UniTask.NextFrame();
        ResourceCounterUtility.CountResources(_moneyAmountText, _timeUpdateResources,
            _startAmountMoney, wonMoney);
    }

    public void SetWonAmountMoney(int money)
    {
        _wonMoney = money;
    }

    private void OnDestroy()
    {
  //      _wonMoneyController.WinningMoneyCalculated -= ShowCountingResources;
    }
}