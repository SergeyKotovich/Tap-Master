using UnityEngine;
using VContainer;

public class UIController : MonoBehaviour
{
    [SerializeField] private ResourceCounterView _resourceCounterView;
    [SerializeField] private WalletView _walletView;
    
    [Inject]
    public void Construct(WonMoneyController wonMoneyController, Wallet wallet)
    {
        _resourceCounterView.Initialize(wonMoneyController);
        _walletView.Initialize(wallet);
    }
}