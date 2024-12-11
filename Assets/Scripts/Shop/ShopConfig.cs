using UnityEngine;

[CreateAssetMenu(menuName = "Create ShopConfig", fileName = "ShopConfig", order = 0)]
public class ShopConfig : ScriptableObject
{
    [field: SerializeField] public int CostRockets { get; private set; }
    [field: SerializeField] public int CostLaser { get; private set; }
    [field: SerializeField] public int CostBlackHole { get; private set; }
    [field: SerializeField] public int CostUnlockRockets { get; private set; }
    [field: SerializeField] public int CostUnlockLaser { get; private set; }
    [field: SerializeField] public int CostUnlockBlackHole { get; private set; }
    [field: SerializeField] public int CostStandardSkin { get; private set; }
    
    [field: SerializeField] public int UnlockLevelForRocketBooster { get; private set; }
    [field: SerializeField] public int UnlockLevelForLaserBooster { get; private set; }
    [field: SerializeField] public int UnlockLevelForBlackHoleBooster { get; private set; }
    [field: SerializeField] public int CostStandardBackground { get; private set; }
}