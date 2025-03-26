using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 10f;
    private float damage = 1;

    private Transform target;
    private AudioSource audioSource;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemyHealth = collision.GetComponent<Enemy>();
            if (enemyHealth != null)
            {
                float multiplyDmg = PlayerStats.Instance.damageMultiplier;
                float critChance = PlayerStats.Instance.critChance;

                
                float finalDamage = damage * multiplyDmg;

                
                bool isCritical = Random.Range(0f, 1f) <= critChance;
                if (isCritical)
                {
                    finalDamage *= 2f; 
                    Debug.Log("CRITICAL HIT!"); 
                                                
                }

                enemyHealth.TakeDamage(finalDamage);
            }

            Destroy(gameObject);
        }
    }
}