using System;
using DG.Tweening;
using UnityEngine;

namespace ScreensController
{
    public class ScreensController : MonoBehaviour
    {
        public event Action VictoryScreenLoaded;
        public event Action LevelsScreenOpened;
        
        [SerializeField] private Canvas _victoryScreen;
        [SerializeField] private Canvas _defeatScreen;
        [SerializeField] private Canvas _shopScreen;
        [SerializeField] private Canvas _settingsScreen;
        [SerializeField] private Canvas _levelsScreen;
        [SerializeField] private Canvas _gameIsCompletedScreen;
        
        [SerializeField] private MouseClickHandler _mouseClickHandler;
        [SerializeField] private Rotator _rotator;

        public bool IsAnyWindowOpened;

        public void ShowVictoryScreen()
        {
            HideAllScreens();
            
            _rotator.RotateEnabled(false);
            _mouseClickHandler.ClickEnabled(false);
            _victoryScreen.gameObject.SetActive(true);
            VictoryScreenLoaded?.Invoke();
            IsAnyWindowOpened = true;
        }
        public void HideVictoryScreen()
        {
            _rotator.RotateEnabled(true);
            _mouseClickHandler.ClickEnabled(true);
            _victoryScreen.gameObject.SetActive(false);
            IsAnyWindowOpened = false;

        }

        public void ShowShopScreen()
        {
            //HideAllScreens();

            _rotator.RotateEnabled(false);
            _mouseClickHandler.ClickEnabled(false);
            _shopScreen.gameObject.SetActive(true);
            IsAnyWindowOpened = true;

        }
        
        public void ShowLevelsScreen()
        {
            _rotator.RotateEnabled(false);
            _mouseClickHandler.ClickEnabled(false);
            _levelsScreen.gameObject.SetActive(true);
            LevelsScreenOpened?.Invoke();
            IsAnyWindowOpened = true;
        }
        
        
        public void ShowSettingsScreen()
        {
            HideAllScreens();

            _rotator.RotateEnabled(false);
            _mouseClickHandler.ClickEnabled(false);
            _settingsScreen.gameObject.SetActive(true);
            IsAnyWindowOpened = true;
        }
 
        public void ShowGameCompletedScreen()
        {
            HideAllScreens();
            _gameIsCompletedScreen.gameObject.SetActive(true);
            IsAnyWindowOpened = true;

        }
        
        public void HideGameCompletedScreen()
        {
            HideAllScreens();
            _gameIsCompletedScreen.gameObject.SetActive(false);
            IsAnyWindowOpened = false;

        }

        public void HideAllScreens()
        {
            _rotator.RotateEnabled(true);
            _mouseClickHandler.ClickEnabled(true);
            
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


    }
}