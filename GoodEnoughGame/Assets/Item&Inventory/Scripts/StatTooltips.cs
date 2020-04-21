using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class StatTooltips : MonoBehaviour
{
    [SerializeField]
    Text StatModName;
    [SerializeField]
    Text StatModLabel;
    [SerializeField]
    Text StatModifier;

    private StringBuilder sb = new StringBuilder();

    public void ShowTooltips(CharacterStat stat, string statName)
    {
        StatModName.text = GetStatTopText(stat, statName);

        StatModifier.text = GetStatModifier(stat);

        gameObject.SetActive(true);
    }

    public void HideTooltips()
    {
        gameObject.SetActive(false);
    }

    private string GetStatTopText(CharacterStat stat, string statName)
    {
        sb.Length = 0;
        sb.Append(statName);
        sb.Append(" ");
        sb.Append(stat.Value);
        sb.Append(" (");
        sb.Append(stat.baseValue);

        if (stat.Value > stat.baseValue)
            sb.Append("+");
        
        if (stat.Value - stat.baseValue != 0)
            sb.Append(stat.Value - stat.baseValue);
        sb.Append(")");
        return sb.ToString();
    }

    private string GetStatModifier(CharacterStat stat)
    {
        sb.Length = 0;
        float tree = 0;
        foreach (StatModifier mod in stat.StatModifiers)
        {
            TreeNode node = mod.source as TreeNode;
            if (node != null)
            {
                tree += mod.value;
                continue;
            }

            if (sb.Length != 0)
                sb.AppendLine();

            if (mod.value > 0)
                sb.Append("+");
            sb.Append(mod.value);

            EquipableItem item = mod.source as EquipableItem;

            if (item != null)
            {
                sb.Append(" ");
                sb.Append(item.ItemName);
            }
            else
            {
                Debug.LogError("Modifier is not from an equippable item");
            }
        }
        if (tree != 0)
        {
            if (sb.Length != 0)
                sb.AppendLine();
            sb.Append("+");
            sb.Append(tree);
            sb.Append(" from tree");
        }

        return sb.ToString();
    }
}
