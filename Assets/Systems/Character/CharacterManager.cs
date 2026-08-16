using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] InventorySlotUI weaponSlot;
    [SerializeField] GameObject weapon; 

    bool controllingCharacter = true;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        weaponSlot.OnUpdateItem += EquipWeapon;
    }
    
    void EquipWeapon()
    {
        animator.SetFloat("Weapon", weaponSlot.currentItem != null ? 1 : 0);
        weapon.SetActive(weaponSlot.currentItem != null);
    }
}