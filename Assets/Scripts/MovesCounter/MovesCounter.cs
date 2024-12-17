using System;
using UniTaskPubSub;
using UnityEngine;

public class MovesCounter : ITurnHandler
{
    public event Action<int> CountMovesChanged;

    private int _availableMoves;

    private readonly AsyncMessageBus _messageBus;

    public MovesCounter(AsyncMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public void SetCountMoves(ICountCubesProvider level)
    {
        var countCubes = level.CountCubes;
        var countMoves = Mathf.CeilToInt(countCubes * 1.1f);
        _availableMoves = countMoves;
        CountMovesChanged?.Invoke(_availableMoves);
    }

    public void SpendOneMove()
    {
        _availableMoves--;
        CountMovesChanged?.Invoke(_availableMoves);
        if (_availableMoves == 0)
        {
            _messageBus.Publish(new LevelFailed());
        }
    }

    public bool HasMoves()
    {
        return _availableMoves > 0;
    }
}