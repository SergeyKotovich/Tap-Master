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
            Physics.Raycast(_cube.position, _cube.up, out hit, 15f);
        }

        public void FindObstacleNeighbours()
        {
            CollectNeighboursRecursive(_cube.position,_cube.up, 15f);
        }

        private void CollectNeighboursRecursive(Vector3 cube, Vector3 direction, float maxDistance)
        {
            if (Physics.Raycast(cube,direction, out var hit, maxDistance) && hit.collider != null)
            {
                _shakeAnimationController.AddCubeForAnimation(hit);
                CollectNeighboursRecursive(hit.transform.position, _cube.up, 1);
            }
        }
    }
}