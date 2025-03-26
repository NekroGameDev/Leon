using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Player Stats UI")]
    public TMP_Text livesText;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    [Header("Level Progress UI")]
    public TMP_Text levelText;

    [Header("Round UI")]
    public TMP_Text roundTimerText;
    public TMP_Text roundCounterText;

    [Header("JoyStick")]
    public Joystick playerJoyStick;

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
        InitializeUI();
    }

    private void InitializeUI()
    {
        UpdateAllPlayerStats();
        UpdateLevelProgress();
    }

    #region Event Subscriptions
    void OnEnable()
    {
        GameManager.OnCoinsChanged += UpdateAllPlayerStats;
        GameManager.OnLivesChanged += UpdateAllPlayerStats;
        GameManager.OnScoreChanged += UpdateAllPlayerStats;
        GameManager.OnExpChanged += UpdateLevelProgress;
        GameManager.OnLevelUp += OnLevelUp;
        RoundManager.OnRoundStarted += OnRoundStarted;
    }

    void OnDisable()
    {
        GameManager.OnCoinsChanged -= UpdateAllPlayerStats;
        GameManager.OnLivesChanged -= UpdateAllPlayerStats;
        GameManager.OnScoreChanged -= UpdateAllPlayerStats;
        GameManager.OnExpChanged -= UpdateLevelProgress;
        GameManager.OnLevelUp -= OnLevelUp;
        RoundManager.OnRoundStarted -= OnRoundStarted;
    }
    #endregion

    #region UI Update Methods
    public void UpdateAllPlayerStats()
    {
        if (GameManager.Instance == null) return;

        livesText.text = $"Lives: {PlayerStats.Instance.currentHealth}";
        scoreText.text = $"Score: {GameManager.Instance.GetScore()}";
        highScoreText.text = $"High Score: {GameManager.Instance.GetHighScore()}";
    }

    public void UpdateLevelProgress()
    {
        if (GameManager.Instance == null) return;

        levelText.text = $"Level: {GameManager.Instance.GetCurrentLevel()}";
    }

    public void UpdateRoundTimer(float time)
    {
        roundTimerText.text = Mathf.CeilToInt(time).ToString();
    }
    #endregion

    #region Round and Bonus UI
    private void OnRoundStarted(int roundNumber)
    {
        roundCounterText.text = $"Round: {roundNumber}";
        playerJoyStick.gameObject.SetActive(true);
    }

    public void ShowRoundComplete(int levelsGained)
    {
        playerJoyStick.gameObject.SetActive(false);

        RoundManager.Instance.StartNewRound();
    }

    #endregion

    private void OnLevelUp()
    {
        UpdateLevelProgress();
    }
}
