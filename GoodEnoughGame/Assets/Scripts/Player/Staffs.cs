using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staffs : Weapons
{
    private float movespeed;

    public Staffs()
    {
        fireDamageFlat += Ilvl * 8 + 40;
        waterDamageFlat += Ilvl * 8 + 40;
        airDamageFlat += Ilvl * 8 + 40;

        fireDamagePercent += Ilvl * 0.04f + 0.02f;
        waterDamagePercent += Ilvl * 0.04f + 0.02f;
        airDamagePercent += Ilvl * 0.04f + 0.02f;

        physicalDmgFlat += Ilvl * 2 + 10;
        physicalDmgPercent += Ilvl * 0.002f + 0.04f;

        attackSpeed += Ilvl * 0.01f;
        castSpeed += Ilvl * 0.01f;

        movespeed = (Ilvl * 0.01f + 0.02f) * -1;
    }
}
