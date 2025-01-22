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
        [SerializeField] private UnityEvent OnCloseLevelScreen;
        [SerializeField] private GameObject _victoryScreen;
        [SerializeField] private GameObject _defeatScreen;

        private readonly List<IDisposable> _subscriptions = new();
        private bool _levelCompleted;

        [Inject]
        public void Construct(AsyncMessageBus messageBus)
        {
            _subscriptions.Add(messageBus.Subscribe<LevelCompleteEvent>(_ => ShowVictoryScreen()));
            _subscriptions.Add(messageBus.Subscribe<LevelFailedEvent>(_ => ShowDefeatScreen()));
            _subscriptions.Add(messageBus.Subscribe<LevelSelectedEvent>(_ => CloseLevelScreen()));
        }

        private void CloseLevelScreen()
        {
            OnCloseLevelScreen?.Invoke();
        }

        private async UniTask ShowDefeatScreen()
        {
            await UniTask.Delay(600);
            if (_levelCompleted)
            {
                return;
            }

            _defeatScreen.SetActive(true);
        }

        private async UniTask ShowVictoryScreen()
        {
            _levelCompleted = true;
            _victoryScreen.SetActive(true);
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