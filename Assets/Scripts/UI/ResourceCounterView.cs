using Cube;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using VContainer;

public class ResourceCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyAmountText;
    [SerializeField] private TextMeshProUGUI _cubesAmountText;
    
    [SerializeField] private float _timeUpdateResources;

    private int _wonMoney;
    private float _startAmountMoney;
    private float _startAmountCubes;
    
    private WonMoneyController _wonMoneyController;

    
    public void Initialize(WonMoneyController wonMoneyController)
    {
        _wonMoneyController = wonMoneyController;
        _wonMoneyController.WinningMoneyCalculated += ShowCountingResources;
    }

    private void ShowCountingResources(int wonMoney, int cubesCount)
    {
        _startAmountMoney = 0;
        _startAmountCubes = 0f;
        
        ResourceCounterUtility.CountResources(_cubesAmountText, _timeUpdateResources,
            _startAmountCubes, cubesCount).Forget();
    
        ResourceCounterUtility.CountResources(_moneyAmountText, _timeUpdateResources,
            _startAmountMoney, wonMoney).Forget();
    }

    private void OnDestroy()
    {
        _wonMoneyController.WinningMoneyCalculated -= ShowCountingResources;
    }
}