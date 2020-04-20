using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CharacterStat stat;
    public CharacterStat Stat {
        get { return stat; }
        set
        {
            stat = value;
            UpdateStatValue();
        }
    }


    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            nameText.text = _name;
        }
    }

    [SerializeField] Text nameText;
    [SerializeField] Text valueText;
    [SerializeField]
    StatTooltips tooltips;

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        valueText = texts[1];

        if (tooltips == null)
        {
            tooltips = FindObjectOfType<StatTooltips>();
        }
    }

    public void OnDisable()
    {
        tooltips.HideTooltips();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltips.ShowTooltips(Stat, Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltips.HideTooltips();
    }

    public void UpdateStatValue()
    {
        valueText.text = stat.Value.ToString();
    }
}

