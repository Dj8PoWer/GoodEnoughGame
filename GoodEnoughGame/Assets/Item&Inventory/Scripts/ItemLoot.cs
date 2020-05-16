﻿using Photon.Pun;
using UnityEngine;

public class ItemLoot : MonoBehaviour
{
    private string[] prefixes =
    {
        "Powerful", "Legendary", "Weak", "Broken", "Divine", "Dangerous", "Sharp", "Steel", "Wooden", "Bone", "Copper", "Holy", "Sage", "Vengeful", "Doubting",
        "Whispering", "Muttering", "Wailing", "Deafening", "Screaming", "Shrieking"
    };
    private string[] suffixes =
    {
        "of Doom", "of Power", "of Wisdom", "of Magic", "of Frost", "of Heat", "of Vengeance", "of Friendship", "of Hate", "of the Earth", "of God", "of the Devil",
        "of Fear", "of Anger", "of Anguish", "of Doubt", "of Wrath", "of Envy", "of Greed", "of Hatred", "of Suffering", "of Sorrow"
    };

    [SerializeField]
    Items item;
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    int amount;
    [SerializeField]
    KeyCode key = KeyCode.E;
    [SerializeField]
    private Sprite[] icons;

    public GameObject player;
    SpriteRenderer spritRenderer;

    private bool isInRange;

    private void Start()
    {
        spritRenderer = GetComponent<SpriteRenderer>();
        spritRenderer.sprite = item.Icon;
        GenerateRandomItem(Random.Range(20,200));
        
    }

    private void OnValidate()
    {
        spritRenderer = GetComponent<SpriteRenderer>();
        spritRenderer.sprite = item.Icon;
    }

    private void Update()
    {

        if (player == null)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            PhotonView PV;
            foreach (var player in players)
            {
                PV = player.GetPhotonView();
                if (PV.IsMine)
                {
                    this.player = player;
                }
            }
        }

        if (inventory == null)
            inventory = FindObjectOfType<Character>().Inventory;

        if (isInRange && Input.GetKeyDown(key))
        {
            if (item != null)
            {
                Items itemCopy = item.GetCopy();
                if (inventory.AddItem(itemCopy))
                {
                    amount--;
                    if (amount == 0)
                        Destroy(gameObject);
                }
                else
                {
                    itemCopy.Destroy();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView PVC = collision.gameObject.GetPhotonView();
        if (collision.gameObject == player && PVC.IsMine)
            isInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PhotonView PVC = collision.gameObject.GetPhotonView();
        if (collision.gameObject == player && PVC.IsMine)
            isInRange = false;
    }

    public void GenerateRandomItem(int lvl)
    {
        EquipableItem i = item.GetCopy() as EquipableItem;

        //Select Type
        int select = Random.Range(0, 5);
        i.equipmentType = (EquipmentType)select;

        //Select Icon
        if (i.equipmentType != EquipmentType.Gem)
        {
            //i.Icon = icons[select];
        }

        #region Select Stats FOR ARMOR
        if (i.equipmentType == EquipmentType.Helmet)
        {
            int stats = Random.Range(1, 5);
            for (int j = 0; j < stats; j++)
            {
                int n = Random.Range(0, 7);
                if (n == 0)
                {
                    if (i.hpFlat == 0)
                        i.hpFlat = 10 + 2 * lvl + Random.Range(-2f, 2f) * lvl;
                    else
                        stats++;
                }
                else if (n == 1)
                {
                    if (i.hpPercent == 0)
                        i.hpPercent = 0.02f + 0.005f * lvl + Random.Range(-0.0005f, 0.0005f) * lvl;
                    else
                        stats++;
                }
                else if (n == 2)
                {
                    if (i.hpRegenFlat == 0)
                        i.hpRegenFlat = 5 + 1 * lvl + Random.Range(-1.5f, 1.5f) * lvl;
                    else
                        stats++;
                }
                else if (n == 3)
                {
                    if (i.hpRegenPercent == 0)
                        i.hpRegenPercent = 0.05f + 0.005f * lvl + Random.Range(-0.0005f, 0.0005f) * lvl;
                    else
                        stats++;
                }
                else if (n == 4)
                {
                    if (i.armor == 0)
                        i.armor = 0.02f + 0.005f * lvl + Random.Range(-0.0005f, 0.0005f) * lvl;
                    else
                        stats++;
                }
                else if (n == 5)
                {
                    if (i.airResist == 0)
                        i.airResist = 0.02f + 0.005f * lvl + Random.Range(-0.0005f, 0.0005f) * lvl;
                    else
                        stats++;
                }
                else if (n == 6)
                {
                    if (i.fireResist == 0)
                        i.fireResist = 0.02f + 0.005f * lvl + Random.Range(-0.0005f, 0.0005f) * lvl;
                    else
                        stats++;
                }
                else if (n == 7)
                {
                    if (i.waterResist == 0)
                        i.waterResist = 0.02f + 0.005f * lvl + Random.Range(-0.0005f, 0.0005f) * lvl;
                    else
                        stats++;
                }
            }
            //Select Name
            if (Random.Range(0, 3) < 3)
                i.ItemName += prefixes[Random.Range(0, prefixes.Length - 1)] + " ";
            i.ItemName += "Helmet";
            if (Random.Range(0, 3) < 3)
                i.ItemName += " " + suffixes[Random.Range(0, suffixes.Length - 1)];
            //Select Icon
        }
        else if (i.equipmentType == EquipmentType.Chest)
        {
            int stats = Random.Range(1, 5);
            for (int j = 0; j < stats; j++)
            {
                int n = Random.Range(0, 5);
                if (n == 0)
                {
                    if (i.hpFlat == 0)
                        i.hpFlat = 20 + 4 * lvl + Random.Range(-2f, 4f) * lvl;
                    else
                        stats++;
                }
                else if (n == 1)
                {
                    if (i.hpPercent == 0)
                        i.hpPercent = 0.04f + 0.01f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                    else
                        stats++;
                }
                else if (n == 2)
                {
                    if (i.armor == 0)
                        i.armor = 0.04f + 0.005f * lvl + Random.Range(-0.0008f, 0.0008f) * lvl;
                    else
                        stats++;
                }
                else if (n == 3)
                {
                    if (i.airResist == 0)
                        i.airResist = 0.04f + 0.005f * lvl + Random.Range(-0.0008f, 0.0008f) * lvl;
                    else
                        stats++;
                }
                else if (n == 4)
                {
                    if (i.fireResist == 0)
                        i.fireResist = 0.04f + 0.005f * lvl + Random.Range(-0.0008f, 0.0008f) * lvl;
                    else
                        stats++;
                }
                else if (n == 5)
                {
                    if (i.waterResist == 0)
                        i.waterResist = 0.04f + 0.005f * lvl + Random.Range(-0.0008f, 0.0008f) * lvl;
                    else
                        stats++;
                }
            }
            //Select Name
            if (Random.Range(0, 3) < 3)
                i.ItemName += prefixes[Random.Range(0, prefixes.Length - 1)] + " ";
            i.ItemName += "Chestplate";
            if (Random.Range(0, 3) < 3)
                i.ItemName += " " + suffixes[Random.Range(0, suffixes.Length - 1)];
            //Select Icon
        }
        else if (i.equipmentType == EquipmentType.Glove)
        {
            int stats = Random.Range(1, 5);
            for (int j = 0; j < stats; j++)
            {
                int n = Random.Range(0, 6);
                if (n == 0)
                {
                    if (i.hpFlat == 0)
                        i.hpFlat = 10 + 2 * lvl + Random.Range(-2f, 2f) * lvl;
                    else
                        stats++;
                }
                if (n == 1)
                {
                    if (i.attackSpeed == 0)
                        i.attackSpeed = 0.1f + 0.05f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                    else
                        stats++;
                }
                if (n == 2)
                {
                    if (i.castSpeed == 0)
                        i.castSpeed = 0.1f + 0.05f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                    else
                        stats++;
                }
                else if (n == 3)
                {
                    if (i.armor == 0)
                        i.armor = 0.01f + 0.003f * lvl + Random.Range(-0.0005f, 0.0005f) * lvl;
                    else
                        stats++;
                }
                else if (n == 4)
                {
                    if (i.airResist == 0)
                        i.airResist = 0.01f + 0.003f * lvl + Random.Range(-0.0005f, 0.0005f) * lvl;
                    else
                        stats++;
                }
                else if (n == 5)
                {
                    if (i.fireResist == 0)
                        i.fireResist = 0.01f + 0.003f * lvl + Random.Range(-0.0005f, 0.0005f) * lvl;
                    else
                        stats++;
                }
                else if (n == 6)
                {
                    if (i.waterResist == 0)
                        i.waterResist = 0.01f + 0.003f * lvl + Random.Range(-0.0005f, 0.0005f) * lvl;
                    else
                        stats++;
                }
            }
            //Select Name
            if (Random.Range(0, 3) < 3)
                i.ItemName += prefixes[Random.Range(0, prefixes.Length - 1)] + " ";
            i.ItemName += "Gloves";
            if (Random.Range(0, 3) < 3)
                i.ItemName += " " + suffixes[Random.Range(0, suffixes.Length - 1)];
            //Select Icon
        }
        else if (i.equipmentType == EquipmentType.Boots)
        {
            int stats = Random.Range(1, 5);
            for (int j = 0; j < stats; j++)
            {
                int n = Random.Range(0, 5);
                if (n == 0)
                {
                    if (i.hpFlat == 0)
                        i.hpFlat = 10 + 2 * lvl + Random.Range(-2f, 2f) * lvl;
                    else
                        stats++;
                }
                else if (n == 2)
                {
                    if (i.hpRegenFlat == 0)
                        i.hpRegenFlat = 5 + 1 * lvl + Random.Range(-1.5f, 1.5f) * lvl;
                    else
                        stats++;
                }
                else if (n == 3)
                {
                    if (i.hpRegenPercent == 0)
                        i.hpRegenPercent = 0.05f + 0.005f * lvl + Random.Range(-0.0005f, 0.0005f) * lvl;
                    else
                        stats++;
                }
                else if (n == 4)
                {
                    if (i.armor == 0)
                        i.armor = 0.01f + 0.003f * lvl + Random.Range(-0.0005f, 0.0005f) * lvl;
                    else
                        stats++;
                }
                else if (n == 5)
                {
                    if (i.moveSpeed == 0)
                        i.moveSpeed = 0.1f + 0.05f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                    else
                        stats++;
                }
            }
            //Select Name
            if (Random.Range(0, 3) < 3)
                i.ItemName += prefixes[Random.Range(0, prefixes.Length - 1)] + " ";
            i.ItemName += "Boots";
            if (Random.Range(0, 3) < 3)
                i.ItemName += " " + suffixes[Random.Range(0, suffixes.Length - 1)];
            //Select Icon
        }
        #endregion
        #region Weapon
        else if (i.equipmentType == EquipmentType.Weapon)
        {
            int type = Random.Range(0, 2);
            //Select Weapon Stats
            if (type == 0) //Sword
            {
                int stats = Random.Range(1, 7);
                for (int j = 0; j < stats; j++)
                {
                    int n = Random.Range(0, 12);
                    if (n == 0)
                    {
                        if (i.globalDamage == 0)
                            i.globalDamage = 0.05f + 0.01f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 1)
                    {
                        if (i.hpFlat == 0)
                            i.hpFlat = 10 + 1.5f * lvl + Random.Range(-0.5f, 0.5f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 2)
                    {
                        if (i.fireDamageFlat == 0)
                            i.fireDamageFlat = 5 + 1 * lvl + Random.Range(-1f, 1f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 3)
                    {
                        if (i.fireDamagePercent == 0)
                            i.fireDamagePercent = 0.05f + 0.01f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 4)
                    {
                        if (i.waterDamageFlat == 0)
                            i.waterDamageFlat = 5 + 1 * lvl + Random.Range(-1f, 1f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 5)
                    {
                        if (i.waterDamagePercent == 0)
                            i.waterDamagePercent = 0.03f + 0.008f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 6)
                    {
                        if (i.airDamageFlat == 0)
                            i.airDamageFlat = 7.5f + 1.5f * lvl + Random.Range(-1.5f, 1.5f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 7)
                    {
                        if (i.airDamagePercent == 0)
                            i.airDamagePercent = 0.05f + 0.01f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 8)
                    {
                        if (i.physicalDmgFlat == 0)
                            i.physicalDmgFlat = 10 + 2 * lvl + Random.Range(-2f, 2f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 9)
                    {
                        if (i.physicalDmgPercent == 0)
                            i.physicalDmgPercent = 0.1f + 0.02f * lvl + Random.Range(-0.05f, 0.005f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 10)
                    {
                        if (i.attackSpeed == 0)
                            i.attackSpeed = i.attackSpeed = 0.1f + 0.05f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 11)
                    {
                        if (i.castSpeed == 0)
                            i.castSpeed = i.attackSpeed = 0.05f + 0.02f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 12)
                    {
                        if (i.armor == 0)
                            i.armor = 0.01f + 0.003f * lvl + Random.Range(-0.0005f, 0.0005f) * lvl;
                        else
                            stats++;
                    }
                }
                //Select Name
                if (Random.Range(0, 3) < 3)
                    i.ItemName += prefixes[Random.Range(0, prefixes.Length - 1)] + " ";
                i.ItemName += "Sword";
                if (Random.Range(0, 3) < 3)
                    i.ItemName += " " + suffixes[Random.Range(0, suffixes.Length - 1)];
                //Select Icon
            }
            else if (type == 1) //Bow
            {
                int stats = Random.Range(1, 7);
                for (int j = 0; j < stats; j++)
                {
                    int n = Random.Range(0, 12);
                    if (n == 0)
                    {
                        if (i.globalDamage == 0)
                            i.globalDamage = 0.05f + 0.01f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 1)
                    {
                        if (i.hpFlat == 0)
                            i.hpFlat = 7.5f + 1.2f * lvl + Random.Range(-0.7f, 0.7f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 2)
                    {
                        if (i.fireDamageFlat == 0)
                            i.fireDamageFlat = 5 + 1 * lvl + Random.Range(-1f, 1f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 3)
                    {
                        if (i.fireDamagePercent == 0)
                            i.fireDamagePercent = 0.03f + 0.008f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 4)
                    {
                        if (i.waterDamageFlat == 0)
                            i.waterDamageFlat = 5 + 1 * lvl + Random.Range(-1f, 1f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 5)
                    {
                        if (i.waterDamagePercent == 0)
                            i.waterDamagePercent = 0.03f + 0.008f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 6)
                    {
                        if (i.airDamageFlat == 0)
                            i.airDamageFlat = 10 + 2 * lvl + Random.Range(-2f, 2f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 7)
                    {
                        if (i.airDamagePercent == 0)
                            i.airDamagePercent = 0.1f + 0.02f * lvl + Random.Range(-0.05f, 0.005f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 8)
                    {
                        if (i.physicalDmgFlat == 0)
                            i.physicalDmgFlat = 10 + 2 * lvl + Random.Range(-2f, 2f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 9)
                    {
                        if (i.physicalDmgPercent == 0)
                            i.physicalDmgPercent = 0.03f + 0.008f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 10)
                    {
                        if (i.attackSpeed == 0)
                            i.attackSpeed = 0.15f + 0.07f * lvl + Random.Range(-0.0015f, 0.0015f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 11)
                    {
                        if (i.castSpeed == 0)
                            i.castSpeed = 0.05f + 0.02f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 12)
                    {
                        if (i.moveSpeed == 0)
                            i.moveSpeed = 0.1f + 0.05f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                    }
                }
                //Select Name
                if (Random.Range(0, 3) < 3)
                    i.ItemName += prefixes[Random.Range(0, prefixes.Length - 1)] + " ";
                i.ItemName += "Bow";
                if (Random.Range(0, 3) < 3)
                    i.ItemName += " " + suffixes[Random.Range(0, suffixes.Length - 1)];
                //Select Icon
            }
            else //Staff
            {
                int stats = Random.Range(1, 7);
                for (int j = 0; j < stats; j++)
                {
                    int n = Random.Range(0, 12);
                    if (n == 0)
                    {
                        if (i.globalDamage == 0)
                            i.globalDamage = 0.05f + 0.01f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 1)
                    {
                        if (i.hpFlat == 0)
                            i.hpFlat = 7.5f + 1.2f * lvl + Random.Range(-0.7f, 0.7f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 2)
                    {
                        if (i.fireDamageFlat == 0)
                            i.fireDamageFlat = 10 + 2 * lvl + Random.Range(-2f, 2f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 3)
                    {
                        if (i.fireDamagePercent == 0)
                            i.fireDamagePercent = 0.1f + 0.02f * lvl + Random.Range(-0.05f, 0.005f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 4)
                    {
                        if (i.waterDamageFlat == 0)
                            i.waterDamageFlat = 10 + 2 * lvl + Random.Range(-2f, 2f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 5)
                    {
                        if (i.waterDamagePercent == 0)
                            i.waterDamagePercent = 0.1f + 0.02f * lvl + Random.Range(-0.05f, 0.005f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 6)
                    {
                        if (i.airDamageFlat == 0)
                            i.airDamageFlat = 10 + 2 * lvl + Random.Range(-2f, 2f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 7)
                    {
                        if (i.airDamagePercent == 0)
                            i.airDamagePercent = 0.1f + 0.02f * lvl + Random.Range(-0.05f, 0.005f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 8)
                    {
                        if (i.physicalDmgFlat == 0)
                            i.physicalDmgFlat = 5 + 1 * lvl + Random.Range(-1f, 1f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 9)
                    {
                        if (i.physicalDmgPercent == 0)
                            i.physicalDmgPercent = 0.03f + 0.008f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 10)
                    {
                        if (i.attackSpeed == 0)
                            i.attackSpeed = 0.05f + 0.02f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 11)
                    {
                        if (i.castSpeed == 0)
                            i.castSpeed = 0.15f + 0.07f * lvl + Random.Range(-0.0015f, 0.0015f) * lvl;
                        else
                            stats++;
                    }
                    if (n == 12)
                    {
                        if (i.moveSpeed == 0)
                            i.moveSpeed = 0.1f + 0.05f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                    }
                }
                //Select Name
                if (Random.Range(0, 3) < 3)
                    i.ItemName += prefixes[Random.Range(0, prefixes.Length - 1)] + " ";
                i.ItemName += "Staff";
                if (Random.Range(0, 3) < 3)
                    i.ItemName += " " + suffixes[Random.Range(0, suffixes.Length - 1)];
                //Select Icon
            }
        }
        #endregion
        #region Spell
        else if (i.equipmentType == EquipmentType.Gem)
        {
            //Select Spell
            //Type
            //Icon
            //Stats
        }
        #endregion
        //Select ID
        i.id = "Item" + Random.Range(10000, 99999);

        item = i;
    }
}
