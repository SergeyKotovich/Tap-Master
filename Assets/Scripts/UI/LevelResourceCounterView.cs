using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LevelResourceCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amountMoneyText;
    [SerializeField] private TextMeshProUGUI _amountCubesText;
    [SerializeField] private TextMeshProUGUI _amountPointsText;

    [SerializeField] private float _timeUpdateResources;

    private float _startAmountMoney;
    private float _startAmountCubes;
    private int _startAmountPoints;

    private LevelResourceCounter _levelResourceCounter;


    public void Initialize(LevelResourceCounter levelResourceCounter)
    {
        _levelResourceCounter = levelResourceCounter;
        _levelResourceCounter.ResourceCountStart += ShowCountingResources;
    }

    private void ShowCountingResources(int wonMoney, int cubesCount, int wonPoints)
    {
        _startAmountMoney = 0;
        _startAmountCubes = 0;
        _startAmountPoints = 0;

        ResourceCounterUtility.CountResources(_amountCubesText, _timeUpdateResources,
            _startAmountCubes, cubesCount).Forget();

        ResourceCounterUtility.CountResources(_amountMoneyText, _timeUpdateResources,
            _startAmountMoney, wonMoney).Forget();

        ResourceCounterUtility.CountResources(_amountPointsText, _timeUpdateResources,
                _startAmountPoints, wonPoints).Forget();
    }

    private void OnDestroy()
    {
        _levelResourceCounter.ResourceCountStart -= ShowCountingResources;
    }
}