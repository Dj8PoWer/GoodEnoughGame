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
    private List<statsType> Stats = new List<statsType>();

    #endregion Attributes

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

        for (int i = 0; i < Random.Range(1, possibleStats.Count); i++)
        {
            int index = Random.Range(0, possibleStats.Count - 1);
            Stats.Add(possibleStats[index]);
            possibleStats.RemoveAt(index);
        }

    }

    #endregion Methods


}