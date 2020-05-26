using Photon.Pun;
using System.Collections;
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
    private Sprite[] swordIcons;
    [SerializeField]
    private Sprite[] bowIcons;
    [SerializeField]
    private Sprite[] staffIcons;
    [SerializeField]
    private Sprite[] chestIcons;
    [SerializeField]
    private Sprite[] helmetIcons;
    [SerializeField]
    private Sprite[] gloveIcons;
    [SerializeField]
    private Sprite[] bootsIcons;
    [SerializeField]
    private Sprite[] gemIcons;

    public GameObject player;
    SpriteRenderer spritRenderer;

    private bool isInRange;

    private void Start()
    {
        spritRenderer = GetComponent<SpriteRenderer>();
        spritRenderer.sprite = item.Icon;
        StartCoroutine(SelfDestroy());
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
        int select = Random.Range(0, 10);
        if (select < 5)
            i.equipmentType = (EquipmentType)select;
        else if (select < 8)
            i.equipmentType = EquipmentType.Weapon;
        else
            i.equipmentType = EquipmentType.Gem;


        #region Select Stats, name and icon for weapon/armors
        if (i.equipmentType == EquipmentType.Helmet)
        {
            int stats = Random.Range(1, 5);
            for (int j = 0; j < stats; j++)
            {
                int n = Random.Range(0, 8);
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
                i.ItemName = prefixes[Random.Range(0, prefixes.Length)] + " ";
            i.ItemName += "Helmet";
            if (Random.Range(0, 3) < 3)
                i.ItemName += " " + suffixes[Random.Range(0, suffixes.Length)];
            //Select Icon
            i.Icon = helmetIcons[Random.Range(0, 29)];
        }
        else if (i.equipmentType == EquipmentType.Chest)
        {
            int stats = Random.Range(1, 5);
            for (int j = 0; j < stats; j++)
            {
                int n = Random.Range(0, 6);
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
                i.ItemName = prefixes[Random.Range(0, prefixes.Length)] + " ";
            i.ItemName += "Chestplate";
            if (Random.Range(0, 3) < 3)
                i.ItemName += " " + suffixes[Random.Range(0, suffixes.Length)];
            //Select Icon
            i.Icon = chestIcons[Random.Range(0, 41)];
        }
        else if (i.equipmentType == EquipmentType.Glove)
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
                i.ItemName = prefixes[Random.Range(0, prefixes.Length)] + " ";
            i.ItemName += "Gloves";
            if (Random.Range(0, 3) < 3)
                i.ItemName += " " + suffixes[Random.Range(0, suffixes.Length)];
            //Select Icon
            i.Icon = gloveIcons[Random.Range(0, 20)];
        }
        else if (i.equipmentType == EquipmentType.Boots)
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
                i.ItemName = prefixes[Random.Range(0, prefixes.Length)] + " ";
            i.ItemName += "Boots";
            if (Random.Range(0, 3) < 3)
                i.ItemName += " " + suffixes[Random.Range(0, suffixes.Length)];
            //Select Icon
            i.Icon = bootsIcons[Random.Range(0, 53)];
        }
        else if (i.equipmentType == EquipmentType.Weapon)
        {
            int type = Random.Range(0, 3);
            if (type == 0) //Sword
            {
                int stats = Random.Range(1, 7);
                for (int j = 0; j < stats; j++)
                {
                    int n = Random.Range(0, 13);
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
                    i.ItemName = prefixes[Random.Range(0, prefixes.Length)] + " ";
                i.ItemName += "Sword";
                if (Random.Range(0, 3) < 3)
                    i.ItemName += " " + suffixes[Random.Range(0, suffixes.Length)];
                //Select Icon
                i.Icon = swordIcons[Random.Range(0, 55)];
            }
            else if (type == 1) //Bow
            {
                int stats = Random.Range(1, 7);
                for (int j = 0; j < stats; j++)
                {
                    int n = Random.Range(0, 13);
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
                    i.ItemName = prefixes[Random.Range(0, prefixes.Length)] + " ";
                i.ItemName += "Bow";
                if (Random.Range(0, 3) < 3)
                    i.ItemName += " " + suffixes[Random.Range(0, suffixes.Length)];
                //Select Icon
                i.Icon = bowIcons[Random.Range(0, 20)];

            }
            else //Staff
            {
                int stats = Random.Range(1, 7);
                for (int j = 0; j < stats; j++)
                {
                    int n = Random.Range(0, 13);
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
                            i.fireDamagePercent = 0.1f + 0.02f * lvl + Random.Range(-0.005f, 0.005f) * lvl;
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
                            i.waterDamagePercent = 0.1f + 0.02f * lvl + Random.Range(-0.005f, 0.005f) * lvl;
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
                            i.airDamagePercent = 0.1f + 0.02f * lvl + Random.Range(-0.005f, 0.005f) * lvl;
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
                            i.moveSpeed -= 0.1f + 0.05f * lvl + Random.Range(-0.001f, 0.001f) * lvl;
                        else
                            stats++;
                        if (i.moveSpeed < -.5f)
                            i.moveSpeed = -.5f;
                    }
                }
                //Select Name
                if (Random.Range(0, 3) < 3)
                    i.ItemName = prefixes[Random.Range(0, prefixes.Length)] + " ";
                i.ItemName += "Staff";
                if (Random.Range(0, 3) < 3)
                    i.ItemName += " " + suffixes[Random.Range(0, suffixes.Length)];
                //Select Icon
                i.Icon = staffIcons[Random.Range(0, 18)];

            }
        }
        #endregion
        #region Spell
        else if (i.equipmentType == EquipmentType.Gem)
        {
            string[] spellNames = { "Fireball", "Blade Vortex", "Water Bomb", "Wind Burst" };
            string spell = spellNames[Random.Range(0, spellNames.Length)];

            switch(spell)
            {
                case "Fireball":
                    i.Icon = gemIcons[0];
                    i.fireDamageFlat = 10 + 2 * lvl + Random.Range(-1f, 1f) * lvl;
                    i.ItemName = spell;
                    break;
                case "Blade Vortex":
                    i.Icon = gemIcons[1];
                    i.physicalDmgFlat = 10 + 2 * lvl + Random.Range(-1f, 1f) * lvl;
                    i.ItemName = spell;
                    break;
                case "Water Bomb":
                    i.Icon = gemIcons[2];
                    i.waterDamageFlat = 10 + 2 * lvl + Random.Range(-1f, 1f) * lvl;
                    i.ItemName = spell;
                    break;
                case "Wind Burst":
                    i.Icon = gemIcons[3];
                    i.airDamageFlat = 10 + 2 * lvl + Random.Range(-1f, 1f) * lvl;
                    i.ItemName = spell;
                    break;
            }
            //Icon
            //Stats
        }
        #endregion

        //Select ID
        i.id = "Item" + Random.Range(10000, 99999);

        //Rounding
        i.hpFlat = (float)System.Math.Round(i.hpFlat, 2);
        i.hpPercent = (float)System.Math.Round(i.hpPercent, 2);
        i.hpRegenFlat = (float)System.Math.Round(i.hpRegenFlat, 2);
        i.hpRegenPercent = (float)System.Math.Round(i.hpRegenPercent, 2);
        i.moveSpeed = (float)System.Math.Round(i.moveSpeed, 2);
        i.armor = (float)System.Math.Round(i.armor, 2);
        i.fireResist = (float)System.Math.Round(i.fireResist, 2);
        i.waterResist = (float)System.Math.Round(i.waterResist, 2);
        i.airResist = (float)System.Math.Round(i.airResist, 2);
        i.attackSpeed = (float)System.Math.Round(i.attackSpeed, 2);
        i.castSpeed = (float)System.Math.Round(i.castSpeed, 2);
        i.globalDamage = (float)System.Math.Round(i.globalDamage, 2);
        i.physicalDmgFlat = (float)System.Math.Round(i.physicalDmgFlat, 2);
        i.physicalDmgPercent = (float)System.Math.Round(i.physicalDmgPercent, 2);
        i.fireDamageFlat = (float)System.Math.Round(i.fireDamageFlat, 2);
        i.waterDamageFlat = (float)System.Math.Round(i.waterDamageFlat, 2);
        i.airDamageFlat = (float)System.Math.Round(i.airDamageFlat, 2);
        i.fireDamagePercent = (float)System.Math.Round(i.fireDamagePercent, 2);
        i.waterDamagePercent = (float)System.Math.Round(i.waterDamagePercent, 2);
        i.airDamagePercent = (float)System.Math.Round(i.airDamagePercent, 2);

        item = i;
        //spritRenderer.sprite = i.Icon;
    }

    IEnumerator SelfDestroy()
    {
        spritRenderer.sprite = item.Icon;
        yield return new WaitForSeconds(30f);
        Destroy(gameObject);
    }

}
