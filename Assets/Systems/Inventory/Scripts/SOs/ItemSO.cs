using UnityEngine;

    public enum ItemType { Generic, Weapon, Consumable, Armor }

[CreateAssetMenu(menuName = "Inventory/Item", fileName = "NewItem")]
public class ItemSO : ScriptableObject
{
    public string id;
    public string itemName = "New Item";
    public Sprite icon;
    [TextArea] public string description;
    public ItemType itemType = ItemType.Generic;
    public bool stackable = true;
    public int maxStack = 99;

    // Called when the item is used by a GameObject (player)
    public virtual void Use(GameObject user, InventorySO inventory)
    {
        // Default: nothing. Override in derived ScriptableObjects.
        Debug.Log($"Using item: {itemName} by {user.name}");
    }
    private void OnEnable()
    {
        if (ReferencesSO.instance != null)
        {
            if (!ReferencesSO.instance.items.Contains(this))
            {
                ReferencesSO.instance.items.Add(this);
            }
        }
    }
    private void OnValidate()
    {
        if (ReferencesSO.instance != null)
        {
            if (!ReferencesSO.instance.items.Contains(this))
            {
                ReferencesSO.instance.items.Add(this);
            }
        }
    }
    private void OnDisable()
    {
        if (ReferencesSO.instance != null)
        {
            if (!ReferencesSO.instance.items.Contains(this))
            {
                ReferencesSO.instance.items.Remove(this);
            }
        }
    }
}