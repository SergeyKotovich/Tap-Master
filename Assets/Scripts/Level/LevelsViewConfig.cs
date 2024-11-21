using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/LevelsViewConfig", fileName = "LevelsViewConfig", order = 1)]
public class LevelsViewConfig : ScriptableObject
{
    [field: SerializeField] public Sprite CompletedLevelSprite;
    [field: SerializeField] public Sprite NotCompletedLevelSprite;
    [field: SerializeField] public Sprite CurrentLevelSprite;
}