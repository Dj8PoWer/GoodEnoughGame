using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chestplate : Armors
{
    // Les attributs en commentaires sont les attributs deja herites

    //private int armor;
    //private int hpFlat;
    //private float hpPercent;
    //private float fireResist;
    //private float waterResist;
    //private float airResist;

    public Chestplate()
    {
        armor += Ilvl * 1 + 50;
        hpFlat += Ilvl * 1 + 500;
        hpPercent += Ilvl * 0.08f + 0.08f;
        fireResist += Ilvl * 0.03f + 0.03f;
        waterResist += Ilvl * 0.03f + 0.03f;
        airResist += Ilvl * 0.03f + 0.03f;
    }

}