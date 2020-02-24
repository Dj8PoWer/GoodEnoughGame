using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    private int ilvl;
    private int hpFlat;
    private float hpPercent;
    private float damage;

    public Items()
    {
        this.hpFlat = ilvl * 100 + 50;
        this.hpPercent = ilvl * 0.01f + 0.02f;
        this.damage = ilvl * 0.02f + 0.05f;
    }

    public int HpFlat => hpFlat;

    public float HpPercent => hpPercent;

    public float Damage => damage;
}
