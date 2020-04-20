using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class ItemTooltips : MonoBehaviour
{
    [SerializeField]
    Text ItemName;
    [SerializeField]
    Text ItemSlot;
    [SerializeField]
    Text ItemStats;

    private StringBuilder sb = new StringBuilder();

    public void ShowTooltips(EquipableItem item)
    {
        ItemName.text = item.ItemName;
        ItemSlot.text = item.equipmentType.ToString();

        sb.Length = 0;

        AddStats(item.hpFlat, "Hp");
        AddStats(item.hpPercent, "Hp %");
        AddStats(item.hpRegenFlat, "Hp Regeneration");
        AddStats(item.hpRegenPercent, "Hp Regeneration %");
        AddStats(item.moveSpeed, "Movement Speed");
        AddStats(item.armor, "Armor");
        AddStats(item.fireResist, "Fire Resistance");
        AddStats(item.waterResist, "Water Resistance");
        AddStats(item.airResist, "Air Resistance");
        AddStats(item.attackSpeed, "Attack Speed");
        AddStats(item.castSpeed, "Cast Speed");
        AddStats(item.globalDamage, "Damage %");
        AddStats(item.physicalDmgFlat, "Physical Damage");
        AddStats(item.physicalDmgPercent, "Physical Damage %");
        AddStats(item.fireDamageFlat, "Fire Damage");
        AddStats(item.waterDamageFlat, "Water Damage");
        AddStats(item.airDamageFlat, "Air Damage");
        AddStats(item.fireDamagePercent, "Fire Damage %");
        AddStats(item.waterDamagePercent, "Water Damage %");
        AddStats(item.airDamagePercent, "Air Damage %");


        ItemStats.text = sb.ToString();
        gameObject.SetActive(true);
    }

    public void HideTooltips()
    {
        gameObject.SetActive(false);
    }

    private void AddStats(float value, string name, bool isPercent = false)
    {
        if (value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();
            if (value > 0)
                sb.Append("+");
            if (isPercent)
                value *= 100;
            sb.Append(value);
            sb.Append(" ");
            sb.Append(name);
        }
    }
}
