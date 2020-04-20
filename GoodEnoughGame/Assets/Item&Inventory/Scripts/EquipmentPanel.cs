using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField]
    Transform equipmentSlotsParent;

    public EquipmentSlot[] EquipmentSlots;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;


    private void Start()
    {
        OnValidate();
        for (int i = 0; i < EquipmentSlots.Length; i++)
        {
            EquipmentSlots[i].OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
            EquipmentSlots[i].OnPointerExitEvent += slot => OnPointerExitEvent(slot);
            EquipmentSlots[i].OnRightClickEvent += slot => OnRightClickEvent(slot);
            EquipmentSlots[i].OnBeginDragEvent += slot => OnBeginDragEvent(slot);
            EquipmentSlots[i].OnEndDragEvent += slot => OnEndDragEvent(slot);
            EquipmentSlots[i].OnDragEvent += slot => OnDragEvent(slot);
            EquipmentSlots[i].OnDropEvent += slot => OnDropEvent(slot);
        }
    }


    private void OnValidate()
    {
        EquipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public bool AddItem(EquipableItem item, out EquipableItem previousItem)
    {
        for (int i = 0; i < EquipmentSlots.Length; i++)
        {
            if (EquipmentSlots[i].equipmentType == item.equipmentType)
            {
                previousItem = (EquipableItem) EquipmentSlots[i].ItemGS;
                EquipmentSlots[i].ItemGS = item;
                return true;
            }
        }
        previousItem = null;
        return false;
    }

    public bool RemoveItem(EquipableItem item)
    {
        for (int i = 0; i < EquipmentSlots.Length; i++)
        {
            if (EquipmentSlots[i].ItemGS == item)
            {
                EquipmentSlots[i].ItemGS = null;
                return true;
            }
        }
        return false;
    }
}
