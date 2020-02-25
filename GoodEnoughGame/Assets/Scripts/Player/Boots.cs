using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : Armors
{
    #region Attributes

    // Les attributs en commentaires sont les attributs deja herites

    //private float hpFlat;
    //private float hpPercent;
    private float hpRegenFlat;
    private float moveSpeed;
    //private float armor;
    private List<statsType> possibleStats = new List<statsType>
    {
        statsType.hpFlat,
        statsType.hpPercent,
        statsType.hpRegenFlat,
        statsType.moveSpeed,
        statsType.armor
    };
    private List<statsType> Stats = new List<statsType>();

    #endregion Attributes

    #region Constructor

    public Boots(int ilvl) : base (ilvl)
    {
        hpFlat += Ilvl * 1f + 150f;
        hpPercent += Ilvl * 0.02f + 0.02f;
        hpRegenFlat = Ilvl * 1f + 10f;
        moveSpeed = Ilvl * 0.05f + 0.05f;
        armor += Ilvl * 1f + 15f;
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
