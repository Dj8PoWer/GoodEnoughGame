using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : Armors
{
    #region Attributes

    // Les attributs en commentaires sont les attributs deja herites

    private float hpRegenFlat;
    private float hpRegenPercent;
    //private float hpPercent;
    //private float hpFlat;
    //private float armor;
    private List<statsType> possibleStats = new List<statsType>
    {   
        statsType.hpRegenFlat,
        statsType.hpRegenPercent,
        statsType.hpPercent,
        statsType.hpFlat,
        statsType.armor 
    };
    private List<(statsType, float)> stats = new List<(statsType, float)>();

    #endregion Attributes

    #region Getters&Setters

    public List<(statsType, float)> Stats => stats;

    #endregion Getters&Setters

    #region Constructor

    public Helmet(int ilvl) : base (ilvl)
    {
        hpRegenFlat = Ilvl * 1f + 20f;
        hpRegenPercent = Ilvl * 0.08f + 0.08f;
        hpPercent += Ilvl * 0.02f;
        hpFlat += Ilvl * 1f + 150f;
        armor += Ilvl * 1f + 20f;
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
                case statsType.hpRegenFlat:
                    stats.Add((statsType.hpRegenFlat, hpRegenFlat));
                    break;
                case statsType.hpRegenPercent:
                    stats.Add((statsType.hpRegenPercent, hpRegenPercent));
                    break;
                case statsType.hpPercent:
                    stats.Add((statsType.hpPercent, hpPercent));
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