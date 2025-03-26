using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 10f;
    [Tooltip("Минимальное значение ввода для движения")]
    public float deadZone = 0.1f;

    [Header("Control References")]
    public Joystick joystick;

    private Vector2 movementInput;
    private Rigidbody2D rb;
    private bool useJoystick = true;

    private void Start()
    {
        speed = PlayerStats.Instance.moveSpeed;
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }

        
        if (joystick == null)
        {
            joystick = FindObjectOfType<Joystick>();
            if (joystick == null)
            {
                Debug.LogWarning("Joystick not found! Falling back to keyboard/touch controls");
                useJoystick = false;
            }
        }
    }

    void Update()
    {
        
        if (useJoystick && joystick != null)
        {
            
            movementInput.x = joystick.Horizontal;
            movementInput.y = joystick.Vertical;

            
            if (movementInput.magnitude < deadZone)
            {
                GetAlternativeInput();
            }
        }
        else
        {
            GetAlternativeInput();
        }

        
        if (movementInput.magnitude > deadZone)
        {
            movementInput.Normalize();
        }
        else
        {
            movementInput = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            rb.MovePosition(rb.position + movementInput * speed * Time.fixedDeltaTime);
        }
    }

    private void GetAlternativeInput()
    {
        float keyboardX = Input.GetAxisRaw("Horizontal");
        float keyboardY = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(keyboardX) > deadZone || Mathf.Abs(keyboardY) > deadZone)
        {
            movementInput.x = keyboardX;
            movementInput.y = keyboardY;
            return;
        }
        
    }
}