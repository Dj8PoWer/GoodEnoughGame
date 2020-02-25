using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gloves : Armors
{
    #region Attributes

    // Les attributs en commentaires sont les attributs deja herites

    private float attackSpeed;
    private float castSpeed;
    //private float hpFlat;
    //private float armor;
    private List<statsType> possibleStats = new List<statsType>
    {
        statsType.attackSpeed,
        statsType.castSpeed,
        statsType.hpFlat,
        statsType.armor
    };
    private List<(statsType, float)> stats = new List<(statsType, float)>();

    #endregion Attributes

    #region Getters&Setters

    public List<(statsType, float)> Stats => stats;

    #endregion Getters&Setters

    #region Constructor

    public Gloves(int ilvl) : base (ilvl)
    {
        attackSpeed = Ilvl * 0.05f + 0.05f;
        castSpeed = Ilvl * 0.03f + 0.03f;
        hpFlat += Ilvl * 1f + 70f;
        armor += Ilvl * 1f + 5f;
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
                case statsType.attackSpeed:
                    stats.Add((statsType.attackSpeed, attackSpeed));
                    break;
                case statsType.castSpeed:
                    stats.Add((statsType.castSpeed, castSpeed));
                    break;
                case statsType.hpFlat:
                    stats.Add((statsType.hpFlat, hpFlat));
                    break;
                case statsType.armor:
                    stats.Add((statsType.armor, armor));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            possibleStats.RemoveAt(index);
        }

    }

    #endregion Methods
}