using System.Collections;
using Cube;
using TMPro;
using UnityEngine;

public class WonMoneyControllerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyAmount;
    [SerializeField] private TextMeshProUGUI _cubesAmount;

    [SerializeField] private CubesController _cubesController;
    [SerializeField] private float _timeUpdateResources;

    private int _wonMoney;
    private float _startAmountMoney;
    private float _startAmountCubes;

    public void ShowCountingCubes()
    {
        _startAmountMoney = 0;
        _startAmountCubes = 0f;
        _moneyAmount.text = _startAmountMoney.ToString("0");
        
        StartCoroutine(ShowCountingCubesCoroutine());
    }

    private IEnumerator ShowCountingCubesCoroutine()
    {
        yield return StartCoroutine(ResourceCounterUtility.CountResources(_cubesAmount,_timeUpdateResources,_startAmountCubes,_cubesController.CountCubsInTotal));
        yield return StartCoroutine(ResourceCounterUtility.CountResources(_moneyAmount,_timeUpdateResources,_startAmountMoney,_wonMoney));
    }
    
   public void SetWonAmountMoney(int money)
   {
       _wonMoney = money;
   }


}