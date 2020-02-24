using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swords : Weapons
{
    private int armor;

    public Swords()
    {
        fireDamageFlat += Ilvl * 1 + 5;
        waterDamageFlat += Ilvl * 1 + 5;
        airDamageFlat += Ilvl * 1 + 5;

        fireDamagePercent += Ilvl * 0.002f + 0.01f;
        waterDamagePercent += Ilvl * 0.002f + 0.01f;
        airDamagePercent += Ilvl * 0.002f + 0.01f;

        physicalDmgFlat += Ilvl * 15 + 80;
        physicalDmgPercent += Ilvl * 0.03f + 0.04f;

        attackSpeed += Ilvl * 0.025f;
        castSpeed += Ilvl * 0.002f;

        armor += Ilvl * 7 + 30;
    }
}
