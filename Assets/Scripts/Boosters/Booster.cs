using UnityEngine;

public abstract class Booster : MonoBehaviour
{
    public bool WasBought { get; }
    public BoostersType Type { get; }
    public int Price { get; }
    public int Count { get; }
    public abstract void AddBooster();
    public abstract void SpendBooster();
    public abstract void Initialize(ShopConfig shopConfig);
}