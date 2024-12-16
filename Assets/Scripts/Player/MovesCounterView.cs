using System;
using TMPro;
using UnityEngine;

public class MovesCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _movesCountLabel;
    
    private MovesCounter _movesCounter;
    
    public void Initialize(MovesCounter movesCounter)
    {
        _movesCounter = movesCounter;
        _movesCounter.CountMovesChanged += UpdateCountMoves;
    }

    private void UpdateCountMoves(int countMoves)
    {
        _movesCountLabel.text = countMoves.ToString();
    }

    private void OnDestroy()
    {
        if (_movesCounter!=null)
        {
            _movesCounter.CountMovesChanged -= UpdateCountMoves;
        }
       
    }
}