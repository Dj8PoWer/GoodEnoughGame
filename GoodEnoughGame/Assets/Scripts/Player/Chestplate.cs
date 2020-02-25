using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chestplate : Armors
{
    #region Attributes

    // Les attributs en commentaires sont les attributs deja herites

    //private float armor;
    //private float hpFlat;
    //private float hpPercent;
    //private float fireResist;
    //private float waterResist;
    //private float airResist;
    private List<statsType> possibleStats = new List<statsType>
    {
        statsType.armor,
        statsType.hpFlat,
        statsType.hpPercent,
        statsType.fireResist,
        statsType.waterResist,
        statsType.airResist
    };
    private List<(statsType, float)> stats = new List<(statsType, float)>();

    #endregion Attributes

    #region Getters&Setters

    public List<(statsType, float)> Stats => stats;

    #endregion Getters&Setters

    #region Constructor

    public Chestplate(int ilvl) : base (ilvl)
    {
        armor += Ilvl * 1f + 50f;
        hpFlat += Ilvl * 1f + 500f;
        hpPercent += Ilvl * 0.08f + 0.08f;
        fireResist += Ilvl * 0.03f + 0.03f;
        waterResist += Ilvl * 0.03f + 0.03f;
        airResist += Ilvl * 0.03f + 0.03f;
    }

    #endregion Constructor

    #region Methods

    private void ChooseStats(List<statsType> possibleStats)
    {

        for (int i = 0; i <= UnityEngine.Random.Range(1, possibleStats.Count); i++)
        {
            int index = UnityEngine.Random.Range(0, possibleStats.Count - 1);

            switch (possibleStats[index])

            {
                case statsType.armor:
                    stats.Add((statsType.armor, armor));
                    break;
                case statsType.hpFlat:
                    stats.Add((statsType.hpFlat, hpFlat));
                    break;
                case statsType.hpPercent:
                    stats.Add((statsType.hpPercent, hpPercent));
                    break;
                case statsType.fireResist:
                    stats.Add((statsType.fireResist, fireResist));
                    break;
                case statsType.waterResist:
                    stats.Add((statsType.waterResist, waterResist));
                    break;
                case statsType.airResist:
                    stats.Add((statsType.airResist, airResist));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            possibleStats.RemoveAt(index);
        }

    }

    #endregion Methods


}