using System;
using DG.Tweening;
using UniTaskPubSub;
using UnityEngine;
using VContainer;

namespace ScreensController
{
    public class ScreensController : MonoBehaviour
    {
        public event Action VictoryScreenLoaded;
        public event Action LevelsScreenOpened;
        
        [SerializeField] private GameObject _victoryScreen;
        [SerializeField] private GameObject _defeatScreen;
        [SerializeField] private GameObject _shopScreen;
        [SerializeField] private GameObject _settingsScreen;
        [SerializeField] private GameObject _levelsScreen;
        [SerializeField] private GameObject _gameCompletedScreen;

        public bool IsAnyWindowOpened;
        private IDisposable _subscriptions;

        [Inject]
        public void Construct(AsyncMessageBus messageBus)
        {
            _subscriptions = messageBus.Subscribe<LevelCompleteEvent>(_ => ShowVictoryScreen());
        }

        private void ShowVictoryScreen()
        {
 //           HideAllScreens();
 
            _victoryScreen.SetActive(true);
            VictoryScreenLoaded?.Invoke();
            IsAnyWindowOpened = true;
        }
        public void HideVictoryScreen()
        {
            _victoryScreen.gameObject.SetActive(false);
            IsAnyWindowOpened = false;

        }

        public void ShowShopScreen()
        {
            //HideAllScreens();
            
            _shopScreen.gameObject.SetActive(true);
            IsAnyWindowOpened = true;

        }
        
        public void ShowLevelsScreen()
        {
            _levelsScreen.gameObject.SetActive(true);
            LevelsScreenOpened?.Invoke();
            IsAnyWindowOpened = true;
        }
        
        
        public void ShowSettingsScreen()
        {
            HideAllScreens();
            
            _settingsScreen.gameObject.SetActive(true);
            IsAnyWindowOpened = true;
        }
 
        public void ShowGameCompletedScreen()
        {
            HideAllScreens();
            _gameCompletedScreen.gameObject.SetActive(true);
            IsAnyWindowOpened = true;

        }
        
        public void HideGameCompletedScreen()
        {
            HideAllScreens();
            _gameCompletedScreen.gameObject.SetActive(false);
            IsAnyWindowOpened = false;

        }

        public void HideAllScreens()
        {
            
            _victoryScreen.gameObject.SetActive(false);
            _settingsScreen.gameObject.SetActive(false);
            _shopScreen.gameObject.SetActive(false);
            _levelsScreen.gameObject.SetActive(false);
            _defeatScreen.gameObject.SetActive(false);
            IsAnyWindowOpened = false;

        }
        
        public void ShowDefeatScreen()
        {
            _defeatScreen.gameObject.SetActive(true);
            IsAnyWindowOpened = true;

        }

        private void OnDestroy()
        {
            _subscriptions?.Dispose();
        }
    }
}