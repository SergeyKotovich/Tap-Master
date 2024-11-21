using System.Collections.Generic;
using UnityEngine;

public class LevelsLoader : MonoBehaviour
{
    private List<Level> _levels;

    public void Initialize(List<Level> levels)
    {
        _levels = levels;
    }

    public void LoadLevel(int indexLevel)
    {
        if (indexLevel >= _levels.Count)
        {
            Debug.Log("Levels is over");
            return;
        }

        _levels[indexLevel].gameObject.SetActive(true);
        _levels[indexLevel].ScatterCubes();
    }
}