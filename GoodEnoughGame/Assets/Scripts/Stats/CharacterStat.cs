using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class CharacterStat
{
    public float baseValue;

    public float Value
    {
        get
        {
            if (isChanged || baseValue != lastBaseValue)
            {
                lastBaseValue = baseValue;
                finalValue = CalculateFinalValue();
                isChanged = false;
            }
            return finalValue;
        }
    }

    protected float finalValue;
    protected float lastBaseValue = float.MinValue;
    protected bool isChanged = true;

    protected readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;
    
    public CharacterStat()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    public CharacterStat(float baseValue) : this()
    {
        this.baseValue = baseValue;
    }

    public virtual void AddModifier(StatModifier modifier)
    {
        isChanged = true;
        statModifiers.Add(modifier);
    }

    public virtual bool RemoveModifier(StatModifier modifier)
    {
        if (statModifiers.Remove(modifier))
        {
            isChanged = true;
            return true;
        }
        return false;   
    }

    public virtual bool RemoveAllModifierFromSource(object source)
    {
        bool didRemove = false;
        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            isChanged = true;
            didRemove = true;
            if (statModifiers[i].source == source)
                statModifiers.RemoveAt(i);
        }
        return didRemove;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = baseValue;
        for (int i = 0; i < statModifiers.Count; i++)
        {
            finalValue += statModifiers[i].value;
        }
        return finalValue;
    }
}
