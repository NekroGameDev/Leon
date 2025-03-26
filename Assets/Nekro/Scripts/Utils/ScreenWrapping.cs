using UnityEngine;

public class ScreenWrapping : MonoBehaviour
{
    private Camera mainCamera;
    private float objectWidth;
    private float objectHeight;
    private bool isWrappingX = false;
    private bool isWrappingY = false;
    private Renderer objectRenderer;

    void Start()
    {
        mainCamera = Camera.main;
        objectRenderer = GetComponent<Renderer>();

        // Получаем размеры объекта (с учетом спрайта)
        if (objectRenderer != null)
        {
            objectWidth = objectRenderer.bounds.extents.x;
            objectHeight = objectRenderer.bounds.extents.y;
        }
        else
        {
            // Если нет Renderer, используем коллайдер
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                objectWidth = collider.bounds.extents.x;
                objectHeight = collider.bounds.extents.y;
            }
            else
            {
                Debug.LogWarning("No Renderer or Collider2D found for screen wrapping calculations");
                objectWidth = 0.5f;
                objectHeight = 0.5f;
            }
        }
    }

    void Update()
    {
        ScreenWrap();
    }

    void ScreenWrap()
    {
        bool isVisible = IsVisibleOnScreen();

        
        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }

        if (isWrappingX && isWrappingY)
        {
            return;
        }

        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        Vector3 newPosition = transform.position;

        // Проверяем выход за границы по X
        if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;
            isWrappingX = true;
        }

        // Проверяем выход за границы по Y
        if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.y = -newPosition.y;
            isWrappingY = true;
        }

        // Применяем новую позицию
        transform.position = newPosition;
    }

    bool IsVisibleOnScreen()
    {
        
        if (objectRenderer != null)
        {
            return objectRenderer.isVisible;
        }

        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        return (viewportPosition.x >= 0 && viewportPosition.x <= 1 &&
                viewportPosition.y >= 0 && viewportPosition.y <= 1);
    }
}