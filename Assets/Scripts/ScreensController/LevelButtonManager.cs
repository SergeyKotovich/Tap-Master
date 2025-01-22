using System;
using TMPro;
using UniTaskPubSub;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonManager : MonoBehaviour
{
    [SerializeField] private Button _openLevel;
    [SerializeField] private GameObject _closedLevel;
    [SerializeField] private TextMeshProUGUI _numberLevel;
    private AsyncMessageBus _messageBus;

    public void Initialize(AsyncMessageBus messageBus, int numberLevel)
    {
        _messageBus = messageBus;
        _openLevel.onClick.AddListener(() => OnLevelButtonClicked(numberLevel));
    }

    public void SwitchLevelAccess(int indexLevel)
    {
        _closedLevel.SetActive(false);
        _openLevel.gameObject.SetActive(true);
        UpdateNumberLevel(indexLevel + 1);
    }

    private void UpdateNumberLevel(int numberLevel)
    {
        _numberLevel.text = numberLevel.ToString();
    }
    private void OnLevelButtonClicked(int numberLevel)
    {
        _messageBus.Publish(new LevelSelectedEvent(numberLevel));
    }
}