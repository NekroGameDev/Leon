using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public GameObject bonusPrefab; 
    public float spawnDelay = 2f; 

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        SpawnBonus();
    }

    
    public void SpawnBonus()
    {
        
        Vector2 spawnPosition = GetRandomPositionOnScreen();

        
        GameObject bonus = Instantiate(bonusPrefab, spawnPosition, Quaternion.identity);

        Bonus bonusScript = bonus.GetComponent<Bonus>();
        if (bonusScript != null)
        {
            int expValue = Random.Range(50, 100); 
            int scoreValue = Random.Range(5, 20); 
            bonusScript.Initialize(this, expValue, scoreValue);
        }
    }

    
    private Vector2 GetRandomPositionOnScreen()
    {
        float screenX = Random.Range(0.1f, 0.9f); 
        float screenY = Random.Range(0.1f, 0.9f);

        Vector2 worldPosition = mainCamera.ViewportToWorldPoint(new Vector2(screenX, screenY));
        return worldPosition;
    }

    
    public void BonusCollected()
    {
        
        Invoke("SpawnBonus", spawnDelay);
    }
}