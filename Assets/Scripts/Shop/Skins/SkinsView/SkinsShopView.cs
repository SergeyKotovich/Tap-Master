using TMPro;
using UnityEngine;

public class SkinsShopView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI [] _costs;

    public void Initialize(ShopPricesConfig shopPricesConfig)
    {
        foreach (var cost in _costs)
        {
            cost.text = shopPricesConfig.CostStandardSkin.ToString();
        }

        
    }
}