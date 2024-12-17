using System;
using TMPro;
using UnityEngine;
using VContainer;

public class LevelTimerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeLabel;
    private LevelTimer _levelTimer;

    [Inject]
    public void Construct(LevelTimer levelTimer)
    {
        _levelTimer = levelTimer;
        _levelTimer.TimeChanged += UpdateTime;
    }

    private void UpdateTime(float currentTime)
    {
        _timeLabel.text = currentTime.ToString("F1");
    }

    private void OnDestroy()
    {
        if (_levelTimer!=null)
        {
            _levelTimer.TimeChanged -= UpdateTime;
        }
    }
}