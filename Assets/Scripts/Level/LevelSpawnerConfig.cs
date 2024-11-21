using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelSpawnerConfig", fileName = "LevelSpawnerConfig", order = 0)]
public class LevelSpawnerConfig : ScriptableObject
{
    [field: SerializeField] public float StartCubePosition { get; private set; }
}