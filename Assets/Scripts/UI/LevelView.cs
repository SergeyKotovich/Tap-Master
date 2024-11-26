using System;
using TMPro;
using UnityEngine;

public class LevelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentLevelLabel;

    public void SetLevelNumber(int currentLevel)
    {
        _currentLevelLabel.text = currentLevel.ToString();
    }
}