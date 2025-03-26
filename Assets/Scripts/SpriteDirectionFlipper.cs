using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteDirectionFlipper : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Минимальная скорость для определения направления")]
    public float minSpeedForFlip = 0.1f;
    [Tooltip("Флипать по оси X (горизонтально)")]
    public bool flipX = true;
    [Tooltip("Флипать по оси Y (вертикально)")]
    public bool flipY = false;

    private SpriteRenderer spriteRenderer;
    private Vector3 lastPosition;
    private bool isFacingRight = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 movementDirection = currentPosition - lastPosition;

        if (movementDirection.magnitude >= minSpeedForFlip)
        {
            UpdateFacingDirection(movementDirection);
        }

        lastPosition = currentPosition;
    }

    private void UpdateFacingDirection(Vector3 direction)
    {
        if (flipX)
        {
            bool shouldFaceRight = direction.x > 0;

            if (shouldFaceRight != isFacingRight)
            {
                isFacingRight = shouldFaceRight;
                spriteRenderer.flipX = !isFacingRight;
            }
        }

        if (flipY)
        {
            bool shouldFaceUp = direction.y > 0;
            spriteRenderer.flipY = !shouldFaceUp;
        }
    }

    public void ForceFlip(bool faceRight)
    {
        if (isFacingRight != faceRight)
        {
            isFacingRight = faceRight;
            spriteRenderer.flipX = !isFacingRight;
        }
    }
}