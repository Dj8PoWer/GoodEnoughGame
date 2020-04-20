using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public Items item;

    [SerializeField]
    private Image image;
    [SerializeField]
    private Text amountText;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    bool isPointerOver;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);

    public Items ItemGS
    {
        get { return item; }
        set {
            item = value;

            //if (item == null && _amount > 0) Amount = 0;

            if (item == null)
            {
                image.color = disabledColor;
            }
            else
            {
                image.sprite = item.Icon;
                image.color = normalColor;
            }

            if (isPointerOver)
            {
                OnPointerExit(null);
                OnPointerEnter(null);
            }
        }
    }

    private int _amount;
    public int Amount
    {
        get { return _amount; }
        set
        {

            _amount = value;
            if (_amount < 0) _amount = 0;
            //if (_amount == 0) ItemGS = null;

            //if (_amount == 0 && item != null) ItemGS = null;

            if (amountText != null)
            {
                amountText.enabled = item != null && _amount > 1;
                if (amountText.enabled)
                {
                    amountText.text = _amount.ToString();
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null)
            {
                //OnRightClickEvent(this);
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();

        if (amountText == null)
            amountText = GetComponentInChildren<Text>();
    }

    protected virtual void OnDisable()
    {
        if (isPointerOver)
            OnPointerExit(null);
    }

    public bool CanAddStacks(Items item, int amount = 1)
    {
        return ItemGS != null && ItemGS.ID == item.ID && Amount + amount <= item.maxStack;
    }

    public virtual bool CanRecieveItem(Items item)
    {
        return true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;

        if (OnPointerEnterEvent != null)
            OnPointerEnterEvent(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;

        if (OnPointerExitEvent != null)
            OnPointerExitEvent(this);
    }

    Vector2 originalPostion;

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropEvent(this);
    }
}
