using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _blackHoleCount;
    [SerializeField] private TextMeshProUGUI _laserCount;
    [SerializeField] private TextMeshProUGUI _rocketCount;
    [SerializeField] private Button _rocketButton;
    [SerializeField] private Button _laserButton;
    [SerializeField] private Button _blackHoleButton;

    private IInventoryHandler _inventory;
    private ShopConfig _shopConfig;

    public void Initialize(IInventoryHandler inventory, ShopConfig shopConfig)
    {
        _shopConfig = shopConfig;
        _inventory = inventory;
        _inventory.CountBoostersChanged += UpdateCountBoosters;
        _inventory.BoostersDepleted += DisableInteractableButton;
    }

    private void DisableInteractableButton(Booster booster)
    {
        switch (booster.Type)
        {
            case BoostersType.Laser:
                _laserButton.interactable = false;
                break;
            case BoostersType.Rocket:
                _rocketButton.interactable = false;
                break;
            case BoostersType.BlackHole:
                _blackHoleButton.interactable = false;
                break;
        }
    }

    public void UpdateInfo(int levelNumber)
    {
        if (levelNumber >= _shopConfig.UnlockLevelForRocketBooster)
        {
            _rocketButton.gameObject.SetActive(true);
        }

        if (levelNumber >= _shopConfig.UnlockLevelForLaserBooster)
        {
            _laserButton.gameObject.SetActive(true);
        }

        if (levelNumber >= _shopConfig.UnlockLevelForBlackHoleBooster)
        {
            _blackHoleButton.gameObject.SetActive(true);
        }
    }

    private void UpdateCountBoosters(Booster booster)
    {
        switch (booster.Type)
        {
            case BoostersType.Laser:
                _laserButton.interactable = true;
                _laserCount.text = booster.Count.ToString();
                break;
            case BoostersType.Rocket:
                _rocketButton.interactable = true;
                _rocketCount.text = booster.Count.ToString();
                break;
            case BoostersType.BlackHole:
                _blackHoleButton.interactable = true;
                _blackHoleCount.text = booster.Count.ToString();
                break;
        }
    }

    private void OnDestroy()
    {
        if (_inventory == null)
        {
            return;
        }
        
        _inventory.CountBoostersChanged -= UpdateCountBoosters;
        _inventory.BoostersDepleted -= DisableInteractableButton;

    }
}