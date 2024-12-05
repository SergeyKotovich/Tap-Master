using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BoosterShopView : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _costBuyingBooster;
    [SerializeField] protected TextMeshProUGUI _costUnlockBooster;
    [SerializeField] protected TextMeshProUGUI _unlockLevel;
    [SerializeField] private Button _boosterButton;
    [SerializeField] private GameObject _activeBoosterPlate;
    [SerializeField] private GameObject _inactiveBoosterPlate;

    public abstract void Initialize(ShopConfig shopConfig);
    
    protected void MakeAvailable()
    {
        _activeBoosterPlate.SetActive(true);
        _inactiveBoosterPlate.SetActive(false);
    }

    public virtual void CanUnlockBooster(int currentLevel)
    {
        _boosterButton.gameObject.SetActive(true); 
    }
}