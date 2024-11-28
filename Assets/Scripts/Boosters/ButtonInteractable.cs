using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteractable : MonoBehaviour
{
    [SerializeField] private int _delay;
    [SerializeField] private Button _button;

    [UsedImplicitly]
    public void DisableInteractable()
    {
        DisableInteractableAsync().Forget();
    }

    private async UniTask DisableInteractableAsync()
    {
        _button.interactable = false;
        await UniTask.Delay(_delay);
        _button.interactable = true;
    }
}