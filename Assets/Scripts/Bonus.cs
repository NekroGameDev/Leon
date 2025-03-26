using UnityEngine;

public class Bonus : MonoBehaviour
{
    private int scoreValue; 
    private int expValue;

    private BonusSpawner spawner; 

    public void Initialize(BonusSpawner spawner, int expValue, int scoreValue)
    {
        this.spawner = spawner;
        this.expValue = expValue;
        this.scoreValue = scoreValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddExp(expValue);
            GameManager.Instance.AddScore(scoreValue);

            spawner.BonusCollected();

            Destroy(gameObject);
        }
    }
}