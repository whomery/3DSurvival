using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform slotPanel;
    public Transform dropPosition;

    [Header("Selected Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedStatName;
    public TextMeshProUGUI selectedStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unequipButton;
    public GameObject dropButton;


    private PlayerController controller;
    private PlayerCondition condition;

    ItemData selectedltem;
    int selectedltemIndex = 0;

    void Start()
    {
        
        controller = ChharacterManager.Instance.Player.controller;
        condition = ChharacterManager.Instance.Player.condition;
        dropPosition = ChharacterManager.Instance.Player.dropPosition;

        controller.inventory += Toggle;

        ChharacterManager.Instance.Player.addItem += Addltem;

        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }

        ClearSelectedItemWindow();
        UpdateUI();
    }

    void Update()
    {
        // 여기에 필요 시 인벤토리 업데이트 로직 추가
    }

    void ClearSelectedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unequipButton.SetActive(false);
        dropButton.SetActive(false);
    }
    public void Toggle()
    {
        if (IsOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }
    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
        
    }
    void Addltem()
    {
        ItemData data = ChharacterManager.Instance.Player.ItemData;

        if (data.canStack)
        {
            ItemSlot slot = GetltemStack(data);
            if (slot != null)
            {
                slot.quantity++;
                UpdateUI();
                ChharacterManager.Instance.Player.ItemData = null;
                return;
            }
        }
        ItemSlot emptySlot = GetEmptySlot();
        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            ChharacterManager.Instance.Player.ItemData = null ;
            return;
        }

        Throwltem(data);
        ChharacterManager.Instance.Player.ItemData = null;
    }
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
                {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }
    ItemSlot GetltemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
            return null;
    }
    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
            return null;
    }
    void Throwltem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }
    public void Selectltem(int index)
    {
        if (slots[index].item == null) return;

        selectedltem = slots[index].item;
        selectedltemIndex = index;

        selectedItemName.text = selectedltem.displayName;
        selectedItemDescription.text = selectedltem.description;

        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;
        for (int i = 0; i < selectedltem.consumables.Length; i++)
        {
            selectedStatName.text += selectedltem.consumables[i].type.ToString() + "Wn";
            selectedStatValue.text += selectedltem.consumables[i].value.ToString() + "Wn";
        }
        useButton.SetActive(selectedltem.type == ItemType.Consumable);
        equipButton.SetActive(selectedltem.type == ItemType.Equipable && !slots[index].equipped);
        unequipButton.SetActive(selectedltem.type == ItemType.Equipable && slots[index].equipped);
        dropButton.SetActive(true);

    }
    void RemoveSelectedltem()
    {
        slots[selectedltemIndex].quantity--;

        if (slots[selectedltemIndex].quantity <= 0)
        {
            selectedltem = null;
            slots[selectedltemIndex].item = null;
            selectedltemIndex = -1;
            ClearSelectedItemWindow();
        }
        UpdateUI();
    }
    public void OnDropButton()
    {
        Throwltem(selectedltem);
        RemoveSelectedltem();
    }
    public void OnUseButton()
    {
        if (selectedltem.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedltem.consumables.Length; i++)
            {
                switch (selectedltem.consumables[i].type)
                {
                    case ConsumableType.Health:
                        condition.Heal(selectedltem.consumables[i].value);
                        break;
                    case ConsumableType.Hunger:
                        condition.Eat(selectedltem.consumables[i].value);
                        break;
                    case ConsumableType.AddSpeed:
                        condition.Eat2(selectedltem.consumables[i].value);
                        break;
                }
            }
            RemoveSelectedltem();
        }
    }
}
