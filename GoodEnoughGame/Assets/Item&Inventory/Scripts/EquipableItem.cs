using UnityEngine;

public enum EquipmentType
{
    Helmet,
    Chest,
    Glove,
    Boots,
    Weapon
}

[CreateAssetMenu]
public class EquipableItem : Items
{
    public int StrengthBonus;
    public int IntelligenceBonus;
    public int AgilityBonus;
    public int VitalityBonus;
    [Space]
    public int StrengthPercentBonus;
    public int IntelligencePercentBonus;
    public int AgilityPercentBonus;
    public int VitalityPercentBonus;
    [Space]
    public EquipmentType equipmentType;

    public void Equip(Character c)
    {
        if (StrengthBonus != 0)
            c.strength.AddModifier(new StatModifier(StrengthBonus, this));
        if (IntelligenceBonus != 0)
            c.intelligence.AddModifier(new StatModifier(IntelligenceBonus, this));
        if (AgilityBonus != 0)
            c.agility.AddModifier(new StatModifier(AgilityBonus, this));
        if (VitalityBonus != 0)
            c.vitality.AddModifier(new StatModifier(VitalityBonus, this));

        if (StrengthPercentBonus != 0)
            c.strength.AddModifier(new StatModifier(StrengthPercentBonus, this));
        if (IntelligencePercentBonus != 0)
            c.intelligence.AddModifier(new StatModifier(IntelligencePercentBonus, this));
        if (AgilityPercentBonus != 0)
            c.strength.AddModifier(new StatModifier(AgilityPercentBonus, this));
        if (VitalityPercentBonus != 0)
            c.vitality.AddModifier(new StatModifier(VitalityPercentBonus, this));
    }

    public void Unequip(Character c)
    {
        c.strength.RemoveAllModifierFromSource(this);
        c.intelligence.RemoveAllModifierFromSource(this);
        c.agility.RemoveAllModifierFromSource(this);
        c.vitality.RemoveAllModifierFromSource(this);
    }
}
