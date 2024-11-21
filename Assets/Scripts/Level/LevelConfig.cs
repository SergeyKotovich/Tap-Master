using UnityEngine;
[CreateAssetMenu(fileName = "LevelConfig", menuName = "ScriptableObjects/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [field:SerializeField] public int LevelVictoryReward { get; private set; }
    [field:SerializeField] public int AvailableMoves { get; private set; }
}
