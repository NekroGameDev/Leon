using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;

    [Header("Round Settings")]
    public float initialRoundTime = 60f;
    public float timeIncreasePerRound = 5f;

    [Header("UI Elements")]
    public TextMeshProUGUI roundTimerText;
    public TextMeshProUGUI roundCounterText;
    public Slider roundTimerSlider;

    private float currentRoundTime;
    public int currentRound = 0;
    private int levelsGainedThisRound = 0;
    private bool isRoundActive = false;

    public static event Action<int> OnRoundStarted;
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

    private void Start()
    {
        StartNewRound();
    }

    private void Update()
    {
        if (isRoundActive)
        {
            currentRoundTime -= Time.deltaTime;
            roundTimerText.text = Mathf.CeilToInt(currentRoundTime).ToString();

            roundTimerSlider.maxValue = initialRoundTime + (currentRound * timeIncreasePerRound);
            roundTimerSlider.value = currentRoundTime;

            if (currentRoundTime <= 0)
            {
                EndRound();
            }
        }
    }

    public void StartNewRound()
    {
        currentRound++;
        currentRoundTime = initialRoundTime + (currentRound * timeIncreasePerRound);
        levelsGainedThisRound = 0;
        isRoundActive = true;
        OnRoundStarted?.Invoke(currentRound);
        roundCounterText.text = $"Round: {currentRound}";
    }

    public void EndRound()
    {
        isRoundActive = false;
        DestroyAllEnemies();

        UIManager.Instance.ShowRoundComplete(levelsGainedThisRound);
    }

    private void DestroyAllEnemies()
    {
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in allEnemies)
        {
            Destroy(enemy.gameObject);
        }
    }

    public void OnLevelGained()
    {
        if (isRoundActive)
        {
            levelsGainedThisRound++;
        }
    }
}