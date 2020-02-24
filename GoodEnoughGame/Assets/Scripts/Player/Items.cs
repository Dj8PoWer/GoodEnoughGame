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
        this.hpFlat = ilvl * 100 + 50;
        this.hpPercent = ilvl * 0.01f + 0.02f;
        this.globalDamage = ilvl * 0.02f + 0.05f;
    }

    public int Ilvl => ilvl;

    public int HpFlat => hpFlat;

    public float HpPercent => hpPercent;

    public float GlobalDamage => globalDamage;
}
