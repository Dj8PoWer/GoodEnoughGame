using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    protected enum statsType{
        hpFlat,
        hpPercent,
        hpRegenFlat,
        hpRegenPercent,
        moveSpeed,

        armor,
        fireResist,
        waterResist,
        airResist,

        attackSpeed,
        castSpeed,

        globalDamage,

        physicalDmgFlat,
        physicalDmgPercent,

        fireDamageFlat,
        waterDamageFlat,
        airDamageFlat,

        fireDamagePercent,
        waterDamagePercent,
        airDamagePercent    
    }

    private int ilvl;
    protected float hpFlat;
    protected float hpPercent;
    protected float globalDamage;

    public Items(int ilvl)
    {
        this.ilvl = ilvl;
        hpFlat = ilvl * 100 + 50;
        hpPercent = ilvl * 0.01f + 0.02f;
        globalDamage = ilvl * 0.02f + 0.05f;
    }

    public int Ilvl => ilvl;
}
