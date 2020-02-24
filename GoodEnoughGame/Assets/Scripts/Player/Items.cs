using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    private int ilvl;
    protected int hpFlat;
    protected float hpPercent;
    protected float globalDamage;

    public Items()
    {
        hpFlat = ilvl * 100 + 50;
        hpPercent = ilvl * 0.01f + 0.02f;
        globalDamage = ilvl * 0.02f + 0.05f;
    }

    public int Ilvl => ilvl;
}
