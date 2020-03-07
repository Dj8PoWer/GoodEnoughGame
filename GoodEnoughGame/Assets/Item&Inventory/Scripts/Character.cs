using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    // Stats of the player

    public CharacterStat strength;
    public CharacterStat agility;
    public CharacterStat intelligence;
    public CharacterStat vitality;

    [SerializeField]
    Inventory inventory;
    [SerializeField]
    EquipmentPanel equipmentPanel;
    [SerializeField]
    StatPanel statPanel;

    private void Awake()
    {
        statPanel.SetStats(strength, agility, intelligence, vitality);
        statPanel.UpdateStatValues();

        inventory.OnItemRightClickedEvent += EquipFromInventory;
        inventory.Initialize();
        equipmentPanel.OnItemRightClickedEvent += UnequipFromInventory;
        equipmentPanel.Initialize();
    }

    public void EquipFromInventory(Items item)
    {
        if (item is EquipableItem)
        {
            Equip((EquipableItem)item);
        }
    }

    private void UnequipFromInventory(Items item)
    {
        if (item is EquipableItem)
        {
            Unequip((EquipableItem)item);
        }
    }

    public void Equip(EquipableItem item)
    {
        if (inventory.RemoveItem(item))
        {
            EquipableItem previousItem;
            if (equipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.Equip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
            {
                inventory.AddItem(item);
            }
         }
    }

    public void Unequip (EquipableItem item)
    {
        if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }

}
