using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armors : Items
{
    protected int armor;
    protected float fireResist;
    protected float waterResist;
    protected float airResist;


    public Armors()
    {
        hpFlat += Ilvl * 1 + 100;
        hpPercent += Ilvl * 0.02f + 0.02f;
        armor = Ilvl * 1 + 20;
        fireResist = Ilvl * 0.02f + 0.02f;
        waterResist = Ilvl * 0.02f + 0.02f;
        airResist = Ilvl * 0.02f + 0.02f;
    }


        
}