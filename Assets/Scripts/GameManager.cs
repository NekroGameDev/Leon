using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController playerController;
    public GameObject playerGO;
    public TMP_Text levelXp;

    public static event Action OnCoinsChanged;
    public static event Action OnLivesChanged;
    public static event Action OnScoreChanged;
    public static event Action OnLevelUp;
    public static event Action OnExpChanged;

    [Header("Stats")]
    private int coins = 0;
    private int score = 0;
    private int highScore = 0;
    private int currentExp = 0;
    private int currentLevel = 1;
    private int expToNextLevel = 50;

    [Header("UI")]
    public GameObject gameOverPrefab;
    public Canvas canvas;

    public ParticleSystem levelUpEffect;
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

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
    private void Start()
    {
        expToNextLevel = Mathf.RoundToInt(50 * Mathf.Pow(1.1f, currentLevel - 1));
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        OnCoinsChanged?.Invoke();
    }

    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke();
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
    public void AddExp(int amount)
    {
        currentExp += amount;
        OnExpChanged?.Invoke();

        while (currentExp >= expToNextLevel)
        {
            LevelUp();
            RoundManager.Instance.OnLevelGained(); 
        }

        levelXp.text = string.Format($"Exp: {currentExp}/{expToNextLevel}");
    }

    public void RemoveExp(int amount)
    {
        currentExp -= amount;
        OnExpChanged?.Invoke();
    }
    private void LevelUp()
    {
        currentExp -= expToNextLevel;
        currentLevel++;
        expToNextLevel = Mathf.RoundToInt(expToNextLevel * 1.5f);
        
        LevelUpBonusManager.Instance.OnLevelUp();

        Instantiate(levelUpEffect, playerController.transform.position, Quaternion.identity);

        OnLevelUp?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        PlayerStats.Instance.currentHealth -= damage;
        
        OnLivesChanged?.Invoke();
        if (PlayerStats.Instance.currentHealth <= 0)
        {
            Instantiate(gameOverPrefab,canvas.transform);
            playerGO.SetActive(false);
        }
    }
    public int GetCoins() => coins;
    public int GetScore() => score;
    public int GetHighScore() => highScore;
    public int GetCurrentExp() => currentExp;
    public int GetCurrentLevel() => currentLevel;
    public int GetExpToNextLevel() => expToNextLevel;
}