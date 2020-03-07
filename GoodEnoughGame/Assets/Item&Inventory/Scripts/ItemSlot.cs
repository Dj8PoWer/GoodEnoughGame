using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    private Items item;

    [SerializeField]
    private Image image;

    public event Action<Items> OnRightClickEvent;

    public Items ItemGS
    {
        get { return item; }
        set {
            item = value;
            if (item == null)
            {
                image.enabled = false;
            }
            else
            {
                image.sprite = item.Icon;
                image.enabled = true;
            }
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null && OnRightClickEvent != null)
            {
                OnRightClickEvent(item);
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
}
