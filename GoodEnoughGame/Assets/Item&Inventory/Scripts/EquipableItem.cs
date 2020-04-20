using UnityEditor;
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
    public float hpFlat;
    public float hpPercent;
    public float hpRegenFlat;
    public float hpRegenPercent;
    public float moveSpeed;
    public float armor;
    public float fireResist;
    public float waterResist;
    public float airResist;
    public float attackSpeed;
    public float castSpeed;
    public float globalDamage;
    public float physicalDmgFlat;
    public float physicalDmgPercent;
    public float fireDamageFlat;
    public float waterDamageFlat;
    public float airDamageFlat;
    public float fireDamagePercent;
    public float waterDamagePercent;
    public float airDamagePercent;

    public EquipmentType equipmentType;

    public override Items GetCopy()
    {
        Items item = Instantiate(this);
        item.id = this.id;
        return item;
    }

    public override void Destroy()
    {
        Destroy(this);
    }


    public void Equip(Character c)
    {
        if (hpFlat != 0)
            c.hpFlat.AddModifier(new StatModifier(hpFlat, this));
        if (hpPercent != 0)
            c.hpPercent.AddModifier(new StatModifier(hpPercent, this));
        if (hpRegenFlat != 0)
            c.hpRegenFlat.AddModifier(new StatModifier(hpRegenFlat, this));
        if (hpRegenPercent != 0)
            c.hpRegenPercent.AddModifier(new StatModifier(hpRegenPercent, this));
        if (moveSpeed != 0)
            c.moveSpeed.AddModifier(new StatModifier(moveSpeed, this));
        if (armor != 0)
            c.armor.AddModifier(new StatModifier(armor, this));
        if (fireResist != 0)
            c.fireResist.AddModifier(new StatModifier(fireResist, this));
        if (waterResist != 0)
            c.waterResist.AddModifier(new StatModifier(waterResist, this));
        if (airResist != 0)
            c.airResist.AddModifier(new StatModifier(airResist, this));
        if (attackSpeed != 0)
            c.attackSpeed.AddModifier(new StatModifier(attackSpeed, this));
        if (castSpeed != 0)
            c.castSpeed.AddModifier(new StatModifier(castSpeed, this));
        if (globalDamage != 0)
            c.globalDamage.AddModifier(new StatModifier(globalDamage, this));
        if (physicalDmgFlat != 0)
            c.physicalDmgFlat.AddModifier(new StatModifier(physicalDmgFlat, this));
        if (physicalDmgPercent != 0)
            c.physicalDmgPercent.AddModifier(new StatModifier(physicalDmgPercent, this));
        if (fireDamageFlat != 0)
            c.fireDamageFlat.AddModifier(new StatModifier(fireDamageFlat, this));
        if (waterDamageFlat != 0)
            c.waterDamageFlat.AddModifier(new StatModifier(waterDamageFlat, this));
        if (airDamageFlat != 0)
            c.airDamageFlat.AddModifier(new StatModifier(airDamageFlat, this));
        if (fireDamagePercent != 0)
            c.fireDamagePercent.AddModifier(new StatModifier(fireDamagePercent, this));
        if (waterDamagePercent != 0)
            c.waterDamagePercent.AddModifier(new StatModifier(waterDamagePercent, this));
        if (airDamagePercent != 0)
            c.airDamagePercent.AddModifier(new StatModifier(airDamagePercent, this));
    }

    public void Unequip(Character c)
    {
        c.hpFlat.RemoveAllModifierFromSource(this);
        c.hpPercent.RemoveAllModifierFromSource(this);
        c.hpRegenFlat.RemoveAllModifierFromSource(this);
        c.hpRegenPercent.RemoveAllModifierFromSource(this);
        c.moveSpeed.RemoveAllModifierFromSource(this);
        c.armor.RemoveAllModifierFromSource(this);
        c.fireResist.RemoveAllModifierFromSource(this);
        c.waterResist.RemoveAllModifierFromSource(this);
        c.airResist.RemoveAllModifierFromSource(this);
        c.attackSpeed.RemoveAllModifierFromSource(this);
        c.castSpeed.RemoveAllModifierFromSource(this);
        c.globalDamage.RemoveAllModifierFromSource(this);
        c.physicalDmgFlat.RemoveAllModifierFromSource(this);
        c.physicalDmgPercent.RemoveAllModifierFromSource(this);
        c.fireDamageFlat.RemoveAllModifierFromSource(this);
        c.waterDamageFlat.RemoveAllModifierFromSource(this);
        c.airDamageFlat.RemoveAllModifierFromSource(this);
        c.fireDamagePercent.RemoveAllModifierFromSource(this);
        c.waterDamagePercent.RemoveAllModifierFromSource(this);
        c.airDamagePercent.RemoveAllModifierFromSource(this);
    }
}
