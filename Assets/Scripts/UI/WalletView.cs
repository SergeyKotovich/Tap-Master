using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyAmountText;
    private float _timeUpdateResources = 0.5f;
    private Wallet _wallet;

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;
        _wallet.AmountMoneyUpdated += CalculateMoney;
    }

    private void CalculateMoney(int startMoney, int totalMoney)
    {
        ResourceCounterUtility.CountResources(_moneyAmountText, _timeUpdateResources, startMoney,
            totalMoney).Forget();
    }

    private void OnDestroy()
    {
        if (_wallet != null)
        {
            _wallet.AmountMoneyUpdated -= CalculateMoney;
        }
    }
}