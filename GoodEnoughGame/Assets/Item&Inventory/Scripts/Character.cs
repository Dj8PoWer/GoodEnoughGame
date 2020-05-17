using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public string charName;

    private PlayerManager player;
    
    // Stats of the player

    public CharacterStat hpFlat;
    public CharacterStat hpPercent;
    public CharacterStat hpRegenFlat;
    public CharacterStat hpRegenPercent;
    public CharacterStat moveSpeed;
    public CharacterStat armor;
    public CharacterStat fireResist;
    public CharacterStat waterResist;
    public CharacterStat airResist;
    public CharacterStat attackSpeed;
    public CharacterStat castSpeed;
    public CharacterStat globalDamage;
    public CharacterStat physicalDmgFlat;
    public CharacterStat physicalDmgPercent;
    public CharacterStat fireDamageFlat;
    public CharacterStat waterDamageFlat;
    public CharacterStat airDamageFlat;
    public CharacterStat fireDamagePercent;
    public CharacterStat waterDamagePercent;
    public CharacterStat airDamagePercent;

    #region Inventory

    public Inventory Inventory;
    public EquipmentPanel EquipmentPanel;


    public StatPanel statPanel;
    [SerializeField]
    ItemTooltips itemTooltips;
    [SerializeField]
    Image draggableItem;
    [SerializeField]
    DropItemArea dropItemArea;
    [SerializeField]
    QuestionDialog questionDialog;

    private ItemSlot dragItemSlot;

    private void OnValidate()
    {
        if (itemTooltips == null)
            itemTooltips = FindObjectOfType<ItemTooltips>();
    }

    private void Update()
    {
        if (player == null)
        {
            PlayerManager[] players;
            players = FindObjectsOfType<PlayerManager>();

            foreach (var p in players)
            {
                if (p.gameObject.GetPhotonView().IsMine)
                    player = p;
            }
        }
    }

    private void Awake()
    {
        statPanel.SetStats(hpFlat, hpPercent, hpRegenFlat, hpRegenPercent, moveSpeed, armor, fireResist, waterResist, airResist, attackSpeed, castSpeed, globalDamage, physicalDmgFlat, physicalDmgPercent, fireDamageFlat, waterDamageFlat, airDamageFlat, fireDamagePercent, waterDamagePercent, airDamagePercent);
        statPanel.UpdateStatValues();

        // Setup Events:
        // Right Click
        Inventory.OnRightClickEvent += Equip;
        EquipmentPanel.OnRightClickEvent += Unequip;
        // Pointer Enter 
        Inventory.OnPointerEnterEvent += ShowTooltips;
        EquipmentPanel.OnPointerEnterEvent += ShowTooltips;
        // Pointer Exit 
        Inventory.OnPointerExitEvent += HideTooltips;
        EquipmentPanel.OnPointerExitEvent += HideTooltips;
        // Begin Drag
        Inventory.OnBeginDragEvent += BeginDrag;
        EquipmentPanel.OnBeginDragEvent += BeginDrag;
        // Begin Drag
        Inventory.OnEndDragEvent += EndDrag;
        EquipmentPanel.OnEndDragEvent += EndDrag;
        // Drag
        Inventory.OnDragEvent += Drag;
        EquipmentPanel.OnDragEvent += Drag;
        // Drop
        Inventory.OnDropEvent += Drop;
        EquipmentPanel.OnDropEvent += Drop;
        dropItemArea.OnDropEvent += DropItemOutsideUI;
    }

    private void Equip(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.ItemGS as EquipableItem;
        if (equipableItem != null)
        {
            Equip(equipableItem);
        }
    }

    private void Unequip(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.ItemGS as EquipableItem;
        if (equipableItem != null)
        {
            Unequip(equipableItem);
        }
    }

    private void ShowTooltips(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.ItemGS as EquipableItem;
        if (equipableItem != null)
        {
            itemTooltips.ShowTooltips(equipableItem);
        }
    }

    private void HideTooltips(ItemSlot itemSlot)
    {
        itemTooltips.HideTooltips();
    }

    private void BeginDrag(ItemSlot itemSlot)
    {
        if (itemSlot.ItemGS != null)
        {
            dragItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.ItemGS.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void EndDrag(ItemSlot itemSlot)
    {
        dragItemSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(ItemSlot itemSlot)
    {
        if (draggableItem.enabled)
            draggableItem.transform.position = Input.mousePosition; 
    }

    private void Drop(ItemSlot dropItemSlot)
    {
        if (dragItemSlot == null) return;

        if (dropItemSlot.CanAddStacks(dragItemSlot.ItemGS))
        {
            int numAddableStacks = dropItemSlot.ItemGS.maxStack - dropItemSlot.Amount;
            int stacksToAdd = Mathf.Min(numAddableStacks, dragItemSlot.Amount);

            dropItemSlot.Amount += stacksToAdd;
            dragItemSlot.Amount -= stacksToAdd;
        }

        else if (dropItemSlot.CanRecieveItem(dragItemSlot.ItemGS) && dragItemSlot.CanRecieveItem(dropItemSlot.ItemGS))
        {
            EquipableItem dragItem = dragItemSlot.ItemGS as EquipableItem;
            EquipableItem dropItem = dropItemSlot.ItemGS as EquipableItem;

            if (dragItemSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Unequip(this);
                if (dropItem != null) dropItem.Equip(this);
            }
            if (dropItemSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Equip(this);
                if (dropItem != null) dropItem.Unequip(this);
            }
            statPanel.UpdateStatValues();

            Items draggedItem = dragItemSlot.ItemGS;
            int draggedItemAmount = dragItemSlot.Amount;

            dragItemSlot.ItemGS = dropItemSlot.ItemGS;
            dragItemSlot.Amount = dropItemSlot.Amount;

            dropItemSlot.ItemGS = draggedItem;
            dropItemSlot.Amount = draggedItemAmount;

            if (dragItem.equipmentType == EquipmentType.Weapon)
            {
                string[] name = dragItem.ItemName.Split(' ');
                if (name[0] == "Sword" || name[0] == "Staff" || name[0] == "Bow")
                    player.SwapWeapons(name[0]);
                else
                    player.SwapWeapons(name[1]);
            }
        }
    }

    private void DropItemOutsideUI()
    {
        if (dragItemSlot == null) return;

        questionDialog.Show();
        ItemSlot baseItemSlot = dragItemSlot;
        questionDialog.OnYesEvent += () => DestroyItemInSlot(baseItemSlot);

    }

    private void DestroyItemInSlot(ItemSlot itemSlot)
    {

        itemSlot.ItemGS.Destroy();
        itemSlot.ItemGS = null;
    }

    public void Equip(EquipableItem item)
    {
        if (Inventory.RemoveItem(item))
        {
            EquipableItem previousItem;
            if (EquipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    Inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();

                if (item.equipmentType == EquipmentType.Weapon)
                {
                    string[] name = item.ItemName.Split(' ');
                    if (name[0] == "Sword" || name[0] == "Staff" || name[0] == "Bow")
                        player.SwapWeapons(name[0]);
                    else
                        player.SwapWeapons(name[1]);
                }
            }
            else
            {
                Inventory.AddItem(item);
            }
         }
    }

    public void Unequip (EquipableItem item)
    {
        if (!Inventory.IsFull() && EquipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            Inventory.AddItem(item);
        }
    }

    #endregion
}
