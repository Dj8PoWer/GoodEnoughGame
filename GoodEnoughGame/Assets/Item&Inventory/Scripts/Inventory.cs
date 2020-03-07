using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    List<Items> items;
    [SerializeField]
    Transform itemsParent;
    [SerializeField]
    ItemSlot[] itemSlots;

    public event Action<Items> OnItemRightClickedEvent;


    public void Initialize()
    {
        for (int i = 0; i< itemSlots.Length; i++)
        {
            itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }
    }

    private void OnValidate()
    {
        if (itemsParent != null)
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        RefreshUI();
    }

    private void RefreshUI()
    {
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length; i++)
            itemSlots[i].ItemGS = items[i];
        for (; i < itemSlots.Length; i++)
            itemSlots[i].ItemGS = null;
    }

    public bool AddItem(Items item)
    {
        if (IsFull())
            return false;
        items.Add(item);
        RefreshUI();
        return true;
    }

    public bool RemoveItem(Items item)
    {
        if (items.Remove(item))
        {
            RefreshUI();
            return true;
        }
        return false;
    }

    public bool IsFull()
    {
        return items.Count >= itemSlots.Length;
    }
}
