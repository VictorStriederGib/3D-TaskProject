using UnityEngine;

    [CreateAssetMenu(menuName = "Inventory/Weapon", fileName = "NewWeapon")]
    public class WeaponSO : ItemSO
    {
        public int damage = 10;

        public override void Use(GameObject user, InventorySO inventory)
        {
            base.Use(user,inventory);
            // Default Use: log attack — replace with actual attack implementation
            Debug.Log($"{user.name} attacks with {itemName} for {damage} damage");
        }
    }