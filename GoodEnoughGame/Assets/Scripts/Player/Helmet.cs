using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : Armors
{
    // Les attributs en commentaires sont les attributs deja herites

    private int hpRegenFlat;
    private float hpRegenPercent;
    //private int hpPercent;
    //private int hpFlat;
    //private int armor;


    public Helmet()
    {
        hpRegenFlat = Ilvl * 1 + 20;
        hpRegenPercent = Ilvl * 0.08f + 0.08f;
        hpPercent += Ilvl * 0.02f;
        hpFlat += Ilvl * 1 + 150;
        armor += Ilvl * 1 + 20;
    }

}