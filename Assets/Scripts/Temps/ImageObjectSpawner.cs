using UnityEngine;

public class ImageObjectSpawner : MonoBehaviour
{
    [SerializeField] private Texture2D imageTexture;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Grid gridSize;

   private void Start()
    {
        SpawnObjectsFromImage();
    }

   private void SpawnObjectsFromImage()
    {
        if (imageTexture == null)
        {
            Debug.LogError("Нет текстуры");
            return;
        }

        // Создаем пустой объект, который будет служить контейнером для созданных объектов
        GameObject container = new GameObject("SpawnedObjects");
        container.transform.SetParent(transform); // Делаем контейнер дочерним к текущему объекту

        // Проходим по каждому пикселю изображения
        for (int x = 0; x < imageTexture.width; x++)
        {
            for (int y = 0; y < imageTexture.height; y++)
            {
                Color pixelColor = imageTexture.GetPixel(x, y);

                // Если пиксель черный, создаем объект на сетке grid
                if (pixelColor == Color.black)
                {
                    // Получаем позицию сетки в мировых координатах
                    Vector3Int gridPosition = new Vector3Int(x, 0, y);

                    // Получаем мировые координаты позиции сетки
                    Vector3 worldPosition = gridSize.CellToWorld(gridPosition);

                    // Создаем объект на вычисленной позиции и делаем его дочерним к контейнеру
                    GameObject spawnedObject = Instantiate(objectToSpawn, worldPosition, Quaternion.identity);
                    spawnedObject.transform.SetParent(container.transform);
                }
            }
        }
    }
}