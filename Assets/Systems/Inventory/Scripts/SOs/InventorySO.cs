using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Inventory/Inventory", fileName = "NewInventory")]
public class InventorySO : ScriptableObject
{
    public string id;
    public int capacity = 20;
    public List<InventoryItem> slots = new List<InventoryItem>();

    private void OnEnable()
    {
        if (ReferencesSO.instance != null)
        {
            if (!ReferencesSO.instance.inventories.Contains(this))
            {
                ReferencesSO.instance.inventories.Add(this);
            }
        }
    }
    private void OnValidate()
    {
        if (ReferencesSO.instance != null)
        {
            if (!ReferencesSO.instance.inventories.Contains(this))
            {
                ReferencesSO.instance.inventories.Add(this);
            }
        }
    }
}
[System.Serializable]
public class InventoryItem
{
    public ItemSO item;
    public int quantity;

    public InventoryItem(ItemSO item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}