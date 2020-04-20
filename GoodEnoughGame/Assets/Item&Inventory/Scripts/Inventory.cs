using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    List<Items> startingItems;
    [SerializeField]
    Transform itemsParent;
    public ItemSlot[] ItemSlots;

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
        for (int i = 0; i< ItemSlots.Length; i++)
        {
            ItemSlots[i].OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
            ItemSlots[i].OnPointerExitEvent += slot => OnPointerExitEvent(slot);
            ItemSlots[i].OnRightClickEvent += slot => OnRightClickEvent(slot);
            ItemSlots[i].OnBeginDragEvent += slot => OnBeginDragEvent(slot);
            ItemSlots[i].OnEndDragEvent += slot => OnEndDragEvent(slot);
            ItemSlots[i].OnDragEvent += slot => OnDragEvent(slot);
            ItemSlots[i].OnDropEvent += slot => OnDropEvent(slot);
        }
    }

    private void OnValidate()
    {
        if (itemsParent != null)
            ItemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        SetStartingItems();
    }

    public void Clear()
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            ItemSlots[i].ItemGS = null;
        }
    }

    private void SetStartingItems()
    {
        int i = 0;
        for (; i < startingItems.Count; i++)
        {
            ItemSlots[i].ItemGS = startingItems[i].GetCopy();
        }

        for(; i < ItemSlots.Length; i++)
        {
            ItemSlots[i].ItemGS = null;
        }
    }

    public bool AddItem(Items item)
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if (ItemSlots[i].ItemGS == null || ItemSlots[i].ItemGS.ID == item.ID && ItemSlots[i].Amount < item.maxStack)
            {
                ItemSlots[i].ItemGS = item;
                ItemSlots[i].Amount++;
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(Items item)
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if (ItemSlots[i].ItemGS == item)
            {
                ItemSlots[i].Amount--;
                return true;
            }
        }
        return false;
    }

    public Items RemoveItem(string itemID)
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            Items item = ItemSlots[i].ItemGS;
            if (item != null && item.ID == itemID)
            {
                ItemSlots[i].Amount--;
                return item;
            }
        }
        return null;
    }



    public bool IsFull()
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if (ItemSlots[i].ItemGS == null)
            {
                return false;
            }
        }
        return true;
    }
}
