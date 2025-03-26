using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    [Header("Stats")]
    public float damageMultiplier = 1f;
    public float maxHealth = 100f;
    public float currentHealth = 1f;
    public float moveSpeed = 5f;
    public float attackSpeed = 1f;
    public float critChance = 0.05f;
    public float fireRange = 5f;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}