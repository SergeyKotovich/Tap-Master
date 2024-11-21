using System.Collections;
using TMPro;
using UnityEngine;

public static class ResourceCounterUtility
{
    public static IEnumerator CountResources(TextMeshProUGUI amountText, float timeUpdateResources, float startValue, float targetValue)
    {
        float currentTime = 0;
        
        while (currentTime < timeUpdateResources)
        {
             startValue = Mathf.Lerp(startValue, targetValue, currentTime / timeUpdateResources);

            currentTime += Time.deltaTime;
            amountText.text = startValue.ToString("0");

            yield return null;
        }

        startValue = targetValue;
        amountText.text = startValue.ToString("0");
    }
}