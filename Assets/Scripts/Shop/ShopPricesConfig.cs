using UnityEngine;

[CreateAssetMenu(menuName = "Create ShopPricesConfig", fileName = "ShopPricesConfig", order = 0)]
public class ShopPricesConfig : ScriptableObject
{
    [field: SerializeField] public int CostRockets { get; private set; }
    [field: SerializeField] public int CostLaser { get; private set; }
    [field: SerializeField] public int CostBlackHole { get; private set; }
    [field: SerializeField] public int CostUnlockRockets { get; private set; }
    [field: SerializeField] public int CostUnlockLaser { get; private set; }
    [field: SerializeField] public int CostUnlockBlackHole { get; private set; }
    [field: SerializeField] public int CostStandardSkin { get; private set; }
    
}