using TMPro;
using UnityEngine;

public class SkinsShopView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI [] _costs;

    public void Initialize(ShopConfig shopConfig)
    {
        foreach (var cost in _costs)
        {
            cost.text = shopConfig.CostStandardSkin.ToString();
        }

        
    }
}