using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public static class ResourceCounterUtility
{
    public static async UniTask CountResources
    (
        TextMeshProUGUI amountText,
        float timeUpdateResources,
        float startValue,
        float targetValue)
    {
        float currentTime = 0;

        while (currentTime < timeUpdateResources)
        {
            var progress = currentTime / timeUpdateResources;
            var interpolatedValue = Mathf.Lerp(startValue, targetValue, progress);
            
            amountText.text = interpolatedValue.ToString("0");
            
            currentTime += Time.unscaledDeltaTime;
            
            await UniTask.NextFrame();
        }
        
        amountText.text = targetValue.ToString("0");
    }
}