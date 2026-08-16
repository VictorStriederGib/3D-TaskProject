using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public InventorySO currentInventory;
    public List<InventorySlotUI> slots = new List<InventorySlotUI>();

    private void Start()
    {
        LoadInventory(currentInventory);
    }
    void OnEnable()
    {
        if (currentInventory != null && slots.Count > 0)
        {
            LoadInventory(currentInventory);
        }
    }
    public void LoadInventory(InventorySO inventory)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < inventory.slots.Count)
            {
                slots[i].LoadItem(inventory.slots[i].item, inventory.slots[i].quantity);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
        UpdateAllItens(inventory);
    }
    public void UpdateAllItens(InventorySO inventory)
    {
        if (inventory == null) return;
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < inventory.slots.Count)
            {
            slots[i].UpdateItem(inventory.slots[i].item, inventory.slots[i].quantity);
            slots[i].OnUpdateItem += UpdateSOs;
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
    void UpdateSOs()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            currentInventory.slots[i].item = slots[i].currentItem;
            currentInventory.slots[i].quantity = slots[i].currentQuantity;

            if (currentInventory.slots.Count < slots.Count)
            {
                currentInventory.slots.Add(new InventoryItem(null,0));
            }
        }
    }
}