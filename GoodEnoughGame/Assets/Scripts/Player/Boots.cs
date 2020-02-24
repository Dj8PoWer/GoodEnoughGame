using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : Armors
{
    // Les attributs en commentaires sont les attributs deja herites

    //private int hpFlat;
    //private float hpPercent;
    private int hpRegenFlat;
    private float moveSpeed;
    //private int armor;

    public Boots()
    {
        hpFlat += Ilvl * 1 + 150;
        hpPercent += Ilvl * 0.02f + 0.02f;
        hpRegenFlat = Ilvl * 1 + 10;
        moveSpeed = Ilvl * 0.05f + 0.05f;
        armor += Ilvl * 1 + 15;
    }

}
