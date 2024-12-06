using UnityEngine;

public abstract class Booster : MonoBehaviour
{
    public bool WasBought { get; }
    public BoostersType Type { get; protected set; }
    public int Price { get; protected set; }
    public int Count { get; protected set; }

    public void AddBooster()
    {
        Count++;
    }

    public void SpendBooster()
    {
        Count--;
    }

    public bool HasBooster()
    {
        return Count > 0;
    }

    public abstract void Initialize(ShopConfig shopConfig);
}