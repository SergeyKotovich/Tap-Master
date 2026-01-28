using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BoosterShopView : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _costBuyingBooster;
    [SerializeField] protected TextMeshProUGUI _costUnlockBooster;
    [SerializeField] protected TextMeshProUGUI _unlockLevel;
    [SerializeField] private GameObject _activeBoosterPlate;
    [SerializeField] private GameObject _inactiveBoosterPlate;
    [SerializeField] protected Button buyToUnlockButton;

    public abstract void Initialize(ShopConfig shopConfig);

    protected void MakeAvailable()
    {
        _activeBoosterPlate.SetActive(true);
        _inactiveBoosterPlate.SetActive(false);
    }

    public abstract void CanUnlockBooster(int currentLevel);
}