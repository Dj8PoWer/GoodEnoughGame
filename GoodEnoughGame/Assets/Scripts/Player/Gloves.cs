using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gloves : Armors
{
    // Les attributs en commentaires sont les attributs deja herites

    private float attackSpeed;
    private float castSpeed;
    //private int hpFlat;
    //private int armor;

    public Gloves()
    {
        attackSpeed = Ilvl * 0.05f + 0.05f;
        castSpeed = Ilvl * 0.03f + 0.03f;
        hpFlat += Ilvl * 1 + 70;
        armor += Ilvl * 1 + 5;
    }
}