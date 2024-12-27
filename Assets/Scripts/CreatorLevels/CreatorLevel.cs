using Cysharp.Threading.Tasks;
using UnityEngine;

public class CreatorLevel : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _cubePrefab;
    private ObstacleDetectorr _obstacleDetector;

    // Возможные направления
    private readonly Vector3[] _directions =
    {
        Vector3.forward, Vector3.back, Vector3.right, Vector3.left, Vector3.up, Vector3.down
    };

    private int gridSize = 10;
    private float radius = 3;
    private float thickness = 2f;
    private float height = 5;
    private float turns = 5;
    private float pointsPerTurn = 5;


    private async void Awake()
    {
        _obstacleDetector = new ObstacleDetectorr();

        for (int level = 0; level < 5; level++)
        {
            int size = 9 - level * 2; // Каждый уровень меньше предыдущего
            if (size <= 0) break;

            for (int x = 0; x < size; x++)
            {
                for (int z = 0; z < size; z++)
                {
                    SpawnCubeWithRandomDirection(new Vector3Int(x + level, level, z + level));
                    await UniTask.Delay(10);
                }
            }
        }

        // Ствол дерева
        int trunkX = 9 / 2 - 2 / 2;
        int trunkZ = 9 / 2 - 2 / 2;
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 2; x++)
            {
                for (int z = 0; z < 2; z++)
                {
                    SpawnCubeWithRandomDirection(new Vector3Int(trunkX + x, -y, trunkZ + z));
                    await UniTask.Delay(10);
                }
            }
        }
           
        
    }

    private void SpawnCubeWithRandomDirection(Vector3Int position)
    {
        for (int attempt = 0; attempt < _directions.Length; attempt++)
        {
            var direction = _directions[Random.Range(0, _directions.Length)];
            var cube = Instantiate(_cubePrefab, _parent);
            cube.transform.position = _grid.CellToWorld(position);
            cube.transform.rotation = Quaternion.LookRotation(direction);

            if (_obstacleDetector.CheckOnValid(cube.transform, -cube.transform.forward, 15f))
            {
                return; // Куб успешно размещен
            }

            Debug.Log("Destroy");
            Destroy(cube.gameObject); // Удаляем куб, если направление не подходит
        }

        Debug.LogWarning($"Куб в позиции {position} не может быть размещен без конфликта.");
    }
}


public class ObstacleDetectorr
{
    public bool CheckOnValid(Transform cubeTransform, Vector3 direction, float maxDistance)
    {
        RaycastHit[] hits = Physics.RaycastAll(cubeTransform.position, direction, maxDistance);

        var isValid = true;
        foreach (var hit in hits)
        {
            if (isValid)
            {
                if (hit.transform == cubeTransform)
                {
                    continue;
                }

                if (Vector3.Dot(-hit.transform.forward, -cubeTransform.forward) < -0.9f)
                {
                    isValid = false;
                }
            }

            return false;
        }

        return true;
    }
}