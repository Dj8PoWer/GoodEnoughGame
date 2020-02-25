using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armors : Items
{
    #region Attributes

    protected float armor;
    protected float fireResist;
    protected float waterResist;
    protected float airResist;

    #endregion Attributes

    #region Constructor

    public Armors(int ilvl) : base (ilvl)
    {
        hpFlat += Ilvl * 1 + 100;
        hpPercent += Ilvl * 0.02f + 0.02f;
        armor = Ilvl * 1f + 20f;
        fireResist = Ilvl * 0.02f + 0.02f;
        waterResist = Ilvl * 0.02f + 0.02f;
        airResist = Ilvl * 0.02f + 0.02f;
    }

    #endregion Constructor



}