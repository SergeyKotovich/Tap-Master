using UnityEngine;
using UnityEngine.UI;

public class RoundImage : MonoBehaviour
{
    [SerializeField] private Image imageComponent;
    [SerializeField] private int _radius = 20;
    private void Start()
    {
        // Получаем компонент Image
        imageComponent = GetComponent<Image>();

        // Вызываем функцию, чтобы закруглить края
        RoundCorners();
    }

    private void RoundCorners()
    {
        // Получаем спрайт изображения
        Sprite sprite = imageComponent.sprite;

        // Создаем новый спрайт для применения закругления краев
        Texture2D texture = new Texture2D(sprite.texture.width, sprite.texture.height);

        // Копируем данные из исходного спрайта в новый
        texture.SetPixels(sprite.texture.GetPixels());
        texture.Apply();

        // Применяем закругление краев
        int radius = 730;
        texture = RoundTexture(texture, radius);

        // Применяем изменения к компоненту Image
        Sprite newSprite =
            Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        imageComponent.sprite = newSprite;
    }

    Texture2D RoundTexture(Texture2D texture, int radius)
    {
        int centerX = texture.width / 2;
        int centerY = texture.height / 2;

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                float distance = Mathf.Sqrt((x - centerX) * (x - centerX) + (y - centerY) * (y - centerY));

                // Если пиксель находится за пределами заданного радиуса, делаем его прозрачным
                if (distance > radius)
                {
                    texture.SetPixel(x, y, Color.clear);
                }
            }
        }

        texture.Apply();
        return texture;
    }
}