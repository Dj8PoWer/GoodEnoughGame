using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField]
    Transform equipmentSlotsParent;
    [SerializeField]
    EquipmentSlot[] equipmentSlots;

    public event Action<Items> OnItemRightClickedEvent;


    public void Initialize()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }
    }


    private void OnValidate()
    {
        equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public bool AddItem(EquipableItem item, out EquipableItem previousItem)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].equipmentType == item.equipmentType)
            {
                previousItem = (EquipableItem) equipmentSlots[i].ItemGS;
                equipmentSlots[i].ItemGS = item;
                return true;
            }
        }
        previousItem = null;
        return false;
    }

    public bool RemoveItem(EquipableItem item)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].ItemGS == item)
            {
                equipmentSlots[i].ItemGS = null;
                return true;
            }
        }
        return false;
    }
}
