using System;
using UnityEngine;

public class Background : MonoBehaviour
{
    public event Action BackgroundWasBought;
    [field: SerializeField] public BackgroundsType Type { get; private set; }
    public int Cost { get; private set; }
    public bool WasBought { get; private set; }

    public void MarkAsBought()
    {
        WasBought = true;
        BackgroundWasBought?.Invoke();
    }

    public void Initialize(ShopConfig shopConfig)
    {
        if (Type == BackgroundsType.Stars)
        {
            WasBought = true;
            BackgroundWasBought?.Invoke();
        }

        Cost = shopConfig.CostStandardBackground;
    }
}