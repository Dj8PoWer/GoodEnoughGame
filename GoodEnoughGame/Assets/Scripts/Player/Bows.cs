using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bows : Weapons
{
    private float movespeed;

    public Bows(int ilvl)
        : base (ilvl)
    {
        fireDamageFlat += Ilvl * 5 + 20;
        waterDamageFlat += Ilvl * 5 + 20;
        airDamageFlat += Ilvl * 5 + 20;

        fireDamagePercent += Ilvl * 0.02f + 0.02f;
        waterDamagePercent += Ilvl * 0.02f + 0.02f;
        airDamagePercent += Ilvl * 0.02f + 0.02f;

        physicalDmgFlat += Ilvl * 7 + 30;
        physicalDmgPercent += Ilvl * 0.02f + 0.04f;

        attackSpeed += Ilvl * 0.01f;
        castSpeed += Ilvl * 0.01f;

        movespeed += Ilvl * 0.01f + 0.02f;
    }
}
