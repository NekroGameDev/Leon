using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 10; 
    private float currentHealth; 

    public float speed = 5f; 
    public int damage = 1; 
    public int expReward;  

    private Transform player; 
    private Vector2 direction; 

    RangeAttribute range;

    [SerializeField] private Slider healthSlider;

    void Start()
    {
        
        expReward = Random.Range(5, 20);
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        UpdateHealth();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            GameManager.Instance.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (player == null) return;

        direction = (player.position - transform.position).normalized;

        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealth();
    }

    void UpdateHealth()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Die()
    {
        GameManager.Instance.AddExp(expReward);
        GameManager.Instance.AddScore(10); 
        
        Destroy(gameObject);
    }
}