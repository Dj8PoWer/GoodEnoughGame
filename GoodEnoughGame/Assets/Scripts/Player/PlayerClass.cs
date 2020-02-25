using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    private enum PlClass
    {
        Ranger,
        Warrior,
        Wizard
    }

    private enum ItemType
    {
        Sword,
        Bow,
        Staff,
        Helmet,
        Chestplate,
        Boots,
        Gloves,
    }

    PlClass playerClass;

    Gloves glove;
    Helmet helmet;
    Chestplate chestplate;
    Boots boots;
    Weapons weapon;
    ItemType WeaponType;

    List<Items> inventory;

    // Items stats

    int lvl;

    float hpFlat;
    float hpPercent;
    float hpRegenFlat;
    float hpRegenPercent;
    float moveSpeed;

    float armor;
    float fireResist;
    float waterResist;
    float airResist;

    float attackSpeed;
    float castSpeed;

    public float globalDamage;

    public float physicalDmgFlat;
    public float physicalDmgPercent;

    public float fireDamageFlat;
    public float waterDamageFlat;
    public float airDamageFlat;

    public float fireDamagePercent;
    public float waterDamagePercent;
    public float airDamagePercent;

    // Player health

    public int currentHp;
    public int maxHp;


    // appele quand : nouveau lvl, changement d'item équipé, changement dans le tree.

    public void updateStats()
    {
        //calcul des stats de base avec le lvl
        switch (playerClass)
        {
            case PlClass.Ranger:
                moveSpeed = 5;
                castSpeed = 4;
                attackSpeed = 4;
                maxHp = lvl * 70;
                break;
            case PlClass.Warrior:
                moveSpeed = 4;
                maxHp = lvl * 150;
                castSpeed = 3;
                attackSpeed = 4;
                break;
            case PlClass.Wizard:
                moveSpeed = 3;
                maxHp = lvl * 100;
                castSpeed = 5;
                attackSpeed = 3;
                break;
        }

        //calcul des stats flat
        void GetStats (Items item, ItemType type)
        {
            List<(Items.statsType, float)> itemStats = new List<(Items.statsType, float)>();
            switch (type)
            {
                case ItemType.Sword:
                    itemStats = ((Swords)item).Stats;
                    break;
                case ItemType.Bow:
                    itemStats = ((Bows)item).Stats;
                    break;
                case ItemType.Staff:
                    itemStats = ((Staffs)item).Stats;
                    break;
                case ItemType.Helmet:
                    itemStats = ((Helmet)item).Stats;
                    break;
                case ItemType.Chestplate:
                    itemStats = ((Chestplate)item).Stats;
                    break;
                case ItemType.Boots:
                    itemStats = ((Boots)item).Stats;
                    break;
                case ItemType.Gloves:
                    itemStats = ((Gloves)item).Stats;
                    break;
                default:
                    Debug.Log("Error in GetStats switch");
                    break;
            }

            foreach (var stat in itemStats)
            {
                switch (stat.Item1)
                {
                    case Items.statsType.hpFlat:
                        hpFlat += stat.Item2;
                        break;
                    case Items.statsType.hpPercent:
                        hpPercent += stat.Item2;
                        break;
                    case Items.statsType.moveSpeed:
                        moveSpeed += stat.Item2;
                        break;
                    case Items.statsType.attackSpeed:
                        attackSpeed += stat.Item2;
                        break;
                    case Items.statsType.castSpeed:
                        castSpeed += stat.Item2;
                        break;
                    case Items.statsType.globalDamage:
                        globalDamage += stat.Item2;
                        break;
                    case Items.statsType.physicalDmgFlat:
                        physicalDmgFlat += stat.Item2;
                        break;
                    case Items.statsType.physicalDmgPercent:
                        physicalDmgPercent += stat.Item2;
                        break;
                    case Items.statsType.fireDamageFlat:
                        fireDamageFlat += stat.Item2;
                        break;
                    case Items.statsType.waterDamageFlat:
                        waterDamageFlat += stat.Item2;
                        break;
                    case Items.statsType.airDamageFlat:
                        airDamageFlat += stat.Item2;
                        break;
                    case Items.statsType.fireDamagePercent:
                        fireDamagePercent += stat.Item2;
                        break;
                    case Items.statsType.waterDamagePercent:
                        waterDamagePercent += stat.Item2;
                        break;
                    case Items.statsType.airDamagePercent:
                        airDamagePercent += stat.Item2;
                        break;
                    case Items.statsType.fireResist:
                        fireResist += stat.Item2;
                        break;
                    case Items.statsType.waterResist:
                        waterResist += stat.Item2;
                        break;
                    case Items.statsType.airResist:
                        airResist += stat.Item2;
                        break;
                    case Items.statsType.hpRegenFlat:
                        hpRegenFlat += stat.Item2;
                        break;
                    case Items.statsType.hpRegenPercent:
                        hpRegenPercent += stat.Item2;
                        break;
                    case Items.statsType.armor:
                        armor += stat.Item2;
                        break;
                    default:
                        throw new Exception();

                }
            }

        }

        GetStats(glove, ItemType.Gloves);
        GetStats(helmet, ItemType.Helmet);
        GetStats(chestplate, ItemType.Chestplate);
        GetStats(boots, ItemType.Boots);
        GetStats(weapon, WeaponType);
    }


}
