using UnityEngine;
[CreateAssetMenu(fileName = "LevelConfig", menuName = "ScriptableObjects/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [field:SerializeField] public int LevelVictoryReward { get; private set; }
    [field:SerializeField] public float MoveMultiplier  { get; private set; }
    [field:SerializeField] public float TimeMultiplier  { get; private set; }
}
