using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; 
    private float fireRate = 1f; 
    public string targetTag = "Enemy"; 
    
    [Header("Audio")]
    public AudioClip shootingAudio;
    public AudioSource AudioSource;

    private TargetFinder targetFinder;
    private float nextFireTime = 0f;

    void Start()
    {
        targetFinder = GetComponent<TargetFinder>();
        fireRate = PlayerStats.Instance.attackSpeed;
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            fireRate = PlayerStats.Instance.attackSpeed;
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        float fireRange = PlayerStats.Instance.fireRange;
        
        Transform target = targetFinder.FindNearestTarget(targetTag, fireRange);
        if (target == null) return;
        AudioSource.PlayOneShot(shootingAudio);
        
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        PlayerProjectile projectileScript = projectile.GetComponent<PlayerProjectile>();
        if (projectileScript != null)
        {
            projectileScript.SetTarget(target);
        }
    }
}