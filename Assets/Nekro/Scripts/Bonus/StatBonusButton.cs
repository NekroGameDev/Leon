using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatBonusButton : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject prefabWindow;

    public void Setup(StatBonus bonus, System.Action<StatBonus> onClick)
    {
        icon.sprite = bonus.icon;
        nameText.text = bonus.bonusName;
        descriptionText.text = bonus.description;

        GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        GetComponentInChildren<Button>().onClick.AddListener(() => onClick(bonus));
    }
}