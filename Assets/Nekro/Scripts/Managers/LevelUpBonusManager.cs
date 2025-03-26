using UnityEngine;
using System.Collections.Generic;

public class LevelUpBonusManager : MonoBehaviour
{
    public static LevelUpBonusManager Instance;

    [Header("Настройки UI")]
    public GameObject statBonusWindowPrefab; 
    public Transform uiCanvas;

    [Header("Доступные бонусы")]
    public List<StatBonus> statBonuses; 


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnLevelUp()
    {
        ShowStatBonusWindow();
    }

    private void ShowStatBonusWindow()
    {
        Time.timeScale = 0f;

        GameObject window = Instantiate(statBonusWindowPrefab, uiCanvas);
        StatBonusWindowUI windowUI = window.GetComponent<StatBonusWindowUI>();

        List<StatBonus> randomBonuses = GetRandomStatBonuses(3);
        windowUI.Initialize(randomBonuses, OnStatBonusSelected);
    }

    private List<StatBonus> GetRandomStatBonuses(int count)
    {
        List<StatBonus> availableBonuses = new List<StatBonus>(statBonuses);
        List<StatBonus> selected = new List<StatBonus>();

        count = Mathf.Min(count, availableBonuses.Count);

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, availableBonuses.Count);
            selected.Add(availableBonuses[index]);
            availableBonuses.RemoveAt(index);
        }

        return selected;
    }

    private void OnStatBonusSelected(StatBonus bonus)
    {
        bonus.ApplyBonus(PlayerStats.Instance);

        DestroyAllWithWindowBonus();
        
        Time.timeScale = 1f;
    }

    void DestroyAllWithWindowBonus()
    {
        StatBonusWindowUI[] enemies = FindObjectsOfType<StatBonusWindowUI>(); 
        
        foreach (StatBonusWindowUI enemy in enemies)
        {
            Destroy(enemy.gameObject); 
        }
    }
}