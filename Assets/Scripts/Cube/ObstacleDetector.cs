using UnityEngine;

namespace Cube
{
    public class ObstacleDetector
    {
        private Transform _cube;
        private readonly ShakeAnimationController _shakeAnimationController;

        public ObstacleDetector(ShakeAnimationController shakeAnimationController)
        {
            _shakeAnimationController = shakeAnimationController;
        }

        public void PerformRaycast(out RaycastHit hit, Transform cubePosition)
        {
            _cube = cubePosition;
            Physics.Raycast(_cube.position, -_cube.forward, out hit, 15f);
        }

        public void FindObstacleNeighbours()
        {
            _shakeAnimationController.ResetHits();
            CollectNeighboursRecursive(_cube.position,-_cube.forward, 15f);
        }

        private void CollectNeighboursRecursive(Vector3 cube, Vector3 direction, float maxDistance)
        {
            while (true)
            {
                if (Physics.Raycast(cube, direction, out var hit, maxDistance))
                {
                    if (hit.collider != null)
                    {
                        _shakeAnimationController.AddCubeForAnimation(hit);
                    }

                    cube = hit.transform.position;
                    direction = -_cube.forward;
                    maxDistance = 1;
                    continue;
                }

                break;
            }
        }
    }
}