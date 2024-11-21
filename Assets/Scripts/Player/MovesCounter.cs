using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class MovesCounter : MonoBehaviour
    {
        public event Action AllMovesWasSpent;
        public int AvailableMoves { get; private set; }

        [SerializeField] private TextMeshProUGUI _movesCountLabel;

        public void InitializeMovesCount(int moves)
        {
            AvailableMoves = moves + 15;
            UpdateMoves();
        }

        private void UpdateMoves()
        {
            _movesCountLabel.text = AvailableMoves.ToString();
        }

        public void SpendOneMove()
        { 
            AvailableMoves--;
            if (AvailableMoves < 0) return;
            UpdateMoves();
            
            if (AvailableMoves == 0)
            {
                AllMovesWasSpent?.Invoke();
            }
        }

    }
}