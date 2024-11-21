using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentLevelLable;

    private void Awake()
    {
        UpdateCurrentLevelView();
    }


    public void UpdateCurrentLevelView()
    {
        var currentLevel = PlayerPrefs.GetInt(GlobalConstants.CurrentLevel) + 1;
        _currentLevelLable.text = currentLevel.ToString();
    }
}