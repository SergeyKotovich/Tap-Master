using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniTaskPubSub;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace ScreensController
{
    public class ScreensController : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onCloseLevelScreen;
        [SerializeField] private GameObject _victoryScreen;
        [SerializeField] private GameObject _defeatScreen;

        private readonly List<IDisposable> _subscriptions = new();
        private bool _levelCompleted;
        
        private ScreenInteractionManager _screenInteractionManager;

        [Inject]
        public void Construct(AsyncMessageBus messageBus, ScreenInteractionManager screenInteractionManager)
        {
            _screenInteractionManager = screenInteractionManager;
            _subscriptions.Add(messageBus.Subscribe<LevelCompleteEvent>(_ => ShowVictoryScreen()));
            _subscriptions.Add(messageBus.Subscribe<LevelFailedEvent>(_ => ShowDefeatScreen()));
            _subscriptions.Add(messageBus.Subscribe<LevelSelectedEvent>(_ => CloseLevelScreen()));
        }

        private void CloseLevelScreen()
        {
            _onCloseLevelScreen?.Invoke();
        }

        private async UniTask ShowDefeatScreen()
        {
            await UniTask.Delay(300);
            
            if (_levelCompleted)
            {
                return;
            }
            SoundsManager.Instance.PlayDefeat();
            _defeatScreen.SetActive(true);
            _screenInteractionManager.EnableInteraction(false);
        }

        private async UniTask ShowVictoryScreen()
        {
            await UniTask.Delay(200);
            _levelCompleted = true;
            _victoryScreen.SetActive(true);
            SoundsManager.Instance.PlayVictory();
            _screenInteractionManager.EnableInteraction(false);
            await UniTask.Delay(1000);
            _levelCompleted = false;
        }

        private void OnDestroy()
        {
            if (_subscriptions == null)
            {
                return;
            }

            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose();
            }
        }
    }
}