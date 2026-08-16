using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class SaveInfoManager : MonoBehaviour
    {
        [Header("Save")]
        public SaveInfoSO saveInfo;

        [Header("References")]
        [SerializeField] Transform player;
        [SerializeField] private InventorySlotUI playerEquippedWeaponSlot;
        [SerializeField] private InventoryUI playerUIInventory;
        [SerializeField] private InventorySO playerInventory;

        [Header("Buttons")]
        [SerializeField] private Button saveButton;
        [SerializeField] private Button loadButton;

    void Start()
    {
        saveButton.onClick.AddListener(SaveGame);
        loadButton.onClick.AddListener(LoadGame);
    }
    public void SaveGame()
    {
        saveInfo.playerPosition = player.position;
        saveInfo.playerRotation = player.rotation;

        saveInfo.currentWeapon = playerEquippedWeaponSlot.currentItem;

        saveInfo.playerItens.Clear();saveInfo.ItensQuantity.Clear();
        for (int i =0; i< playerInventory.slots.Count; i++)
        {
            saveInfo.playerItens.Add(playerInventory.slots[i].item);
            saveInfo.ItensQuantity.Add(playerInventory.slots[i].quantity);
        }

        SaveManager.Save(saveInfo);
    }

    public void LoadGame()
    {
        SaveManager.Load(saveInfo);

        player.position = saveInfo.playerPosition;
        player.rotation = saveInfo.playerRotation;
        
        playerEquippedWeaponSlot.LoadItem(saveInfo.currentWeapon, 1);
        playerEquippedWeaponSlot.UpdateItem(saveInfo.currentWeapon, 1);

        playerInventory.slots.Clear();
        for (int i = 0; i < saveInfo.playerItens.Count; i++)
        {
            playerInventory.slots.Add(new InventoryItem(saveInfo.playerItens[i],saveInfo.ItensQuantity[i]));
        }
        
        playerUIInventory.LoadInventory(playerInventory);
    }
}