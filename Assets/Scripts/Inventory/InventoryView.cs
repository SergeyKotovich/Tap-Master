using TMPro;
using UnityEngine;


public class InventoryView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _blackHoleCount;
    [SerializeField] private TextMeshProUGUI _laserCount;
    [SerializeField] private TextMeshProUGUI _rocketCount;

    private IInventoryHandler _inventory;

    public void Initialize(IInventoryHandler inventory)
    {
        _inventory = inventory;
        UpdateCountBoosters();
    }

   

    private void UpdateCountBoosters ()
    {
        foreach (var booster in _inventory.Boosters)
        {
            switch (booster.Type)
            {
                case BoostersType.Laser:
                    _laserCount.text = booster.Count.ToString();
                    break;
                case BoostersType.Rocket:
                    _rocketCount.text = booster.Count.ToString();
                    break;
                case BoostersType.BlackHole:
                    _blackHoleCount.text = booster.Count.ToString();
                    break;
            }
        }
    }
}