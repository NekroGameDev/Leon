using UnityEngine;

[System.Serializable]
public class StatBonus
{
    public string bonusName;
    public Sprite icon;

    [TextArea] public string description;

    public float damageMultiplier = 1f;
    public float healthIncrease = 0f;
    public float speedIncrease = 0f;
    public float attackSpeedIncrease = 1f;
    public float fireRangeIncrease = 0f;

    public void ApplyBonus(PlayerStats stats)
    {

        if (stats == null)
        {
            return;
        }

        if (damageMultiplier > 1f)
        {
            stats.damageMultiplier *= damageMultiplier;
        }

        if (healthIncrease > 0)
        {
            stats.currentHealth += healthIncrease;
            stats.maxHealth += healthIncrease;

            if (stats.currentHealth >= stats.maxHealth)
            {
                stats.currentHealth = stats.maxHealth;
            }
        }

        if (speedIncrease > 0)
        {
            stats.moveSpeed += speedIncrease;
        }

        if (attackSpeedIncrease > 0)
        {
            stats.attackSpeed *= attackSpeedIncrease;
        }
        
        if (fireRangeIncrease > 0)
        {
            stats.fireRange += fireRangeIncrease;
        }
    }
}