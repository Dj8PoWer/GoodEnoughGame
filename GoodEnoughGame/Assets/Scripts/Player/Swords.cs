using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Swords : Weapons
{
    private float armor;
    private int baseDmg;
    
    private List<(statsType, float)> stats = new List<(statsType, float)>();

    private List<statsType> possibleStats = new List<statsType>
    {
        statsType.globalDamage,
        statsType.hpFlat,
        statsType.hpPercent,
        statsType.fireDamageFlat,
        statsType.fireDamagePercent,
        statsType.waterDamageFlat,
        statsType.waterDamagePercent,
        statsType.airDamageFlat,
        statsType.airDamagePercent,
        statsType.physicalDmgFlat,
        statsType.physicalDmgPercent,
        statsType.attackSpeed,
        statsType.castSpeed,
        statsType.armor
    };

    public Swords(int ilvl)
        : base(ilvl)
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

        armor = Ilvl * 7 + 30;
    }

    public List<(statsType, float)> Stats => stats;

    public int BaseDmg => baseDmg;

    private void GetRandomStats()
    {
        for (int i = 0; i <= Random.Range(1, possibleStats.Count - 1); i++)
        {
            int j = Random.Range(0, possibleStats.Count - 1);
            switch (possibleStats[j])
            {
                case statsType.hpFlat:
                    stats.Add((statsType.hpFlat, hpFlat));
                    break;
                case statsType.hpPercent:
                    stats.Add((statsType.hpFlat, hpPercent));
                    break;
                case statsType.armor:
                    stats.Add((statsType.armor, armor));
                    break;
                case statsType.attackSpeed:
                    stats.Add((statsType.attackSpeed, attackSpeed));
                    break;
                case statsType.castSpeed:
                    stats.Add((statsType.castSpeed, castSpeed));
                    break;
                case statsType.globalDamage:
                    stats.Add((statsType.globalDamage, globalDamage));
                    break;
                case statsType.physicalDmgFlat:
                    stats.Add((statsType.physicalDmgFlat, physicalDmgFlat));
                    break;
                case statsType.physicalDmgPercent:
                    stats.Add((statsType.physicalDmgPercent, physicalDmgPercent));
                    break;
                case statsType.fireDamageFlat:
                    stats.Add((statsType.fireDamageFlat, fireDamageFlat));
                    break;
                case statsType.waterDamageFlat:
                    stats.Add((statsType.waterDamageFlat, waterDamageFlat));
                    break;
                case statsType.airDamageFlat:
                    stats.Add((statsType.airDamageFlat, airDamageFlat));
                    break;
                case statsType.fireDamagePercent:
                    stats.Add((statsType.fireDamagePercent, fireDamagePercent));
                    break;
                case statsType.waterDamagePercent:
                    stats.Add((statsType.waterDamagePercent, waterDamagePercent));
                    break;
                case statsType.airDamagePercent:
                    stats.Add((statsType.airDamagePercent, airDamagePercent));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            possibleStats.RemoveAt(j);
        }
    }
}
