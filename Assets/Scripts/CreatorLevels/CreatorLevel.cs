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
    private float radius = 5;
    private float thickness = 0.5f;
    private float height = 6;
    private float turns = 5;
    private float pointsPerTurn = 20;


    private async void Awake()
    {
        _obstacleDetector = new ObstacleDetectorr();

        for (int y = 0; y < height; y++)
        {
            int layerSize = gridSize - y * 2; // Уменьшаем размеры слоя
            if (layerSize <= 0) break;

            for (int x = 0; x < layerSize; x++)
            {
                for (int z = 0; z < layerSize; z++)
                {
                    SpawnCubeWithRandomDirection(new Vector3Int(x + y, y, z + y));
                    await UniTask.Delay(10);
                }
            }
        }

        // Нижняя часть пирамиды
        for (int y = 1; y <= height; y++) // Начинаем с 1, чтобы не дублировать средний слой
        {
            int layerSize = gridSize - y * 2;
            if (layerSize <= 0) break;

            for (int x = 0; x < layerSize; x++)
            {
                for (int z = 0; z < layerSize; z++)
                {
                    SpawnCubeWithRandomDirection(new Vector3Int(x + y, -y, z + y));
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