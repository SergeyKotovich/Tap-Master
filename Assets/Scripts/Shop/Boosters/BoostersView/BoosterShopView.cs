using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public abstract class BoosterShopView : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _costBuyingBooster;
    [SerializeField] protected TextMeshProUGUI _costUnlockBooster;
    [SerializeField] private GameObject _activeBoosterPlate;
    [SerializeField] private GameObject _inactiveBoosterPlate;

    public abstract void Initialize(ShopPricesConfig shopPricesConfig);

    [UsedImplicitly]
    public void MakeAvailable()
    {
        _activeBoosterPlate.SetActive(true);
        _inactiveBoosterPlate.SetActive(false);
    }
}