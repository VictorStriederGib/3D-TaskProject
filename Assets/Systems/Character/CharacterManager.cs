using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] ItemSO Sword, Spear;
    [SerializeField] InventorySlotUI weaponSlot;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    ItemSO CheckEquippedItem()
    {
        if (weaponSlot.currentItem != null)
        {
            if (weaponSlot.currentItem == Sword)
            {
                animator.SetInteger("WeaponType", 1);
                return Sword;
            }
            else if (weaponSlot.currentItem == Spear)
            {
                animator.SetInteger("WeaponType", 2);
                return Spear;
            }
        }

        return null;
    }
}