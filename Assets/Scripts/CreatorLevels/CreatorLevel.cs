using UnityEngine;
using System.Collections.Generic;

public class CreatorLevel : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Vector3[] _rotations; // Forward, Backward, Right, Left, Up, Down

    private int _countCubes = 10;
    private int _count = 10;

    private List<CubeData> _placedCubes = new List<CubeData>();

    private void Awake()
    {
        for (int x = 0; x < _count; x++)
        {
            for (int y = 0; y < _countCubes; y++)
            {
                for (int z = 0; z < _count; z++)
                {
                    var position = new Vector3Int(x, y, z);

                    // Генерируем допустимое направление
                    var selectedRotation = GetValidRotation(position);

                    // Создаём куб
                    var cube = Instantiate(_cubePrefab, _parent);
                    cube.transform.rotation = Quaternion.Euler(selectedRotation);
                    cube.transform.position = _grid.CellToWorld(position);

                    // Сохраняем данные о кубе
                    _placedCubes.Add(new CubeData(position, selectedRotation));
                }
            }
        }
    }

    private Vector3 GetValidRotation(Vector3Int position)
    {
        // Все направления
        var availableRotations = new List<Vector3>(_rotations);

        foreach (var placedCube in _placedCubes)
        {
            if (IsOnSameLine(position, placedCube.Position))
            {
                // Направление уже размещённого куба
                var placedDirection = GetDirectionFromRotation(placedCube.Rotation);

                // Убираем противоположное направление
                var oppositeDirection = -placedDirection;
                availableRotations.Remove(GetRotationFromDirection(oppositeDirection));
            }
        }

        // Если все направления конфликтуют, выбираем случайное (гарантированное размещение)
        if (availableRotations.Count == 0)
        {
            Debug.LogWarning($"Куб в позиции {position} имеет только конфликтующие направления. Выбрано случайное.");
            return _rotations[Random.Range(0, _rotations.Length)];
        }

        // Возвращаем случайное доступное направление
        return availableRotations[Random.Range(0, availableRotations.Count)];
    }

    private bool IsOnSameLine(Vector3Int posA, Vector3Int posB)
    {
        return posA.x == posB.x || posA.y == posB.y || posA.z == posB.z;
    }

    private Vector3Int GetDirectionFromRotation(Vector3 rotation)
    {
        if (rotation == new Vector3(0, 0, 0)) return Vector3Int.forward;    // Forward
        if (rotation == new Vector3(0, 180, 0)) return Vector3Int.back;    // Backward
        if (rotation == new Vector3(0, 90, 0)) return Vector3Int.right;    // Right
        if (rotation == new Vector3(0, 270, 0)) return Vector3Int.left;    // Left
        if (rotation == new Vector3(90, 0, 0)) return Vector3Int.up;       // Up
        if (rotation == new Vector3(-90, 0, 0)) return Vector3Int.down;    // Down

        return Vector3Int.zero;
    }

    private Vector3 GetRotationFromDirection(Vector3Int direction)
    {
        if (direction == Vector3Int.forward) return new Vector3(0, 0, 0);    // Forward
        if (direction == Vector3Int.back) return new Vector3(0, 180, 0);    // Backward
        if (direction == Vector3Int.right) return new Vector3(0, 90, 0);    // Right
        if (direction == Vector3Int.left) return new Vector3(0, 270, 0);    // Left
        if (direction == Vector3Int.up) return new Vector3(90, 0, 0);       // Up
        if (direction == Vector3Int.down) return new Vector3(-90, 0, 0);    // Down

        return Vector3.zero;
    }

    private struct CubeData
    {
        public Vector3Int Position;
        public Vector3 Rotation;

        public CubeData(Vector3Int position, Vector3 rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}
