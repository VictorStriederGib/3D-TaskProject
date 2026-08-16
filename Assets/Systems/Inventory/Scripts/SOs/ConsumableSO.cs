using UnityEngine;

    [CreateAssetMenu(menuName = "Inventory/Consumable", fileName = "NewConsumable")]
    public class ConsumableSO : ItemSO
    {
        void OnEnable()
        {
            itemType = ItemType.Consumable;
        }
        public override void Use(GameObject user, InventorySO inventory)
        {
            base.Use(user, inventory);

            user.GetComponent<InventorySlotUI>().currentQuantity--;
        }
    }