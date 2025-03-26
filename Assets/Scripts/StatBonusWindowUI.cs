using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class StatBonusWindowUI : MonoBehaviour
{
    [Header("Элементы UI")]
    public TextMeshProUGUI titleText;
    public StatBonusButton[] bonusButtons;

    public void Initialize(List<StatBonus> bonuses, System.Action<StatBonus> onSelect)
    {
        titleText.text = "Выберите улучшение:";

        for (int i = 0; i < bonusButtons.Length; i++)
        {
            if (i < bonuses.Count)
            {
                bonusButtons[i].Setup(bonuses[i], onSelect);
                bonusButtons[i].gameObject.SetActive(true);
            }
            else
            {
                bonusButtons[i].gameObject.SetActive(false);
            }
        }
    }
}