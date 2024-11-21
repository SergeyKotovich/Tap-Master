using System.Collections.Generic;
using Cube;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Cube.Cube> _level;
    [SerializeField] private LevelSpawnerConfig _levelSpawnerConfig;
    private readonly List<Vector3> _cubesTargetPositions = new();

    public void Initialize(ObstacleDetector obstacleDetector, ShakeAnimationController shakeAnimationController,
        EffectFactory effectFactory)
    {
        foreach (var cube in _level)
        {
            cube.Initialize(obstacleDetector, shakeAnimationController, effectFactory);
        }
    }

    private async UniTask SetStartPositionsCubes()
    {
        for (int i = 0; i < _level.Count; i++)
        {
            _level[i].transform.DOLocalMove(_cubesTargetPositions[i], 0.3f);
            await UniTask.NextFrame();
        }
    }

    public void ScatterCubes()
    {
        for (var i = 0; i < _level.Count; i++)
        {
            var startPosition = _level[i].transform.localPosition;
            if (i % 2 == 0)
            {
                startPosition.y += _levelSpawnerConfig.StartCubePosition;
            }
            else if (i % 3 == 0)
            {
                startPosition.x += _levelSpawnerConfig.StartCubePosition;
            }
            else if (i % 3 == 1)
            {
                startPosition.x -= _levelSpawnerConfig.StartCubePosition;
            }
            else
            {
                startPosition.y -= _levelSpawnerConfig.StartCubePosition;
            }

            _cubesTargetPositions.Add(_level[i].transform.localPosition);
            _level[i].transform.position = startPosition;
        }

        SetStartPositionsCubes().Forget();
    }
}