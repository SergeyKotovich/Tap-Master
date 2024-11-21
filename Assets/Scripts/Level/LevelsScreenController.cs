using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelsScreenController : MonoBehaviour
{
    [SerializeField] private LevelsViewConfig _levelsViewConfig;
    [SerializeField] private Image[] _LevelsImages;
    [SerializeField] private LevelsSwitcher _levelsSwitcher;
    [SerializeField] private ScreensController.ScreensController _screensController;

    private TextMeshProUGUI[] _levelValueLabels;

    public void Initialize()
    {
        _levelValueLabels = new TextMeshProUGUI[_LevelsImages.Length];

        for (int i = 0; i < _levelValueLabels.Length; i++)
        {
            _levelValueLabels[i] = _LevelsImages[i].GetComponentInChildren<TextMeshProUGUI>();
        }

        UpdateLevelsScreen();
    }

    public void UpdateLevelsScreen()
    {
        SetCompletedLevelImage();
        SetNotCompletedLevelImage();
    }


    public void ResetLevels()
    {
        for (int i = 0; i < _LevelsImages.Length; i++)
        {
            _LevelsImages[i].sprite = _levelsViewConfig.NotCompletedLevelSprite;
            _levelValueLabels[i].text = " ";
        }
    }

    public void TryLoadLevel(int index)
    {
        if (_levelsSwitcher.CanLoadLevel(index))
        {
            _screensController.HideAllScreens();
        }
    }

    private void SetCompletedLevelImage()
    {
        for (int i = 0; i <= PlayerPrefs.GetInt("LastCompletedLevel"); i++)
        {
            var currentLevel = i + 1;
            _LevelsImages[i].sprite = _levelsViewConfig.CompletedLevelSprite;

            _levelValueLabels[i].text = currentLevel.ToString();
        }
    }

    private void SetNotCompletedLevelImage()
    {
        var currentLevelIndex = PlayerPrefs.GetInt("LastCompletedLevel");

        for (int i = currentLevelIndex + 1; i < _LevelsImages.Length; i++)
        {
            _LevelsImages[i].sprite = _levelsViewConfig.NotCompletedLevelSprite;
        }
    }
}