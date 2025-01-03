using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countPoints;
    private float _pointUpdateTime = 0.5f;
    private ScoreController _scoreController;

    public void Initialize(ScoreController scoreController)
    {
        _scoreController = scoreController;
        _scoreController.CountPointsUpdated += UpdateCountPoints;
    }

    private void UpdateCountPoints(int currentCountPoints, int newCountPoints)
    {
        ResourceCounterUtility.CountResources(_countPoints, _pointUpdateTime, currentCountPoints, newCountPoints).Forget();
    }
}