using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    public static List<ItemSO> itens = new List<ItemSO>();
    public static List<InventorySO> inventories = new List<InventorySO>();
    public static void Save(SaveInfoSO saveInfo)
    {
        SaveFile save = new SaveFile();

        //Save player
        save.playerPosition = saveInfo.playerPosition;
        save.playerRotation = saveInfo.playerRotation;

        //Save itens
        save.currentWeaponId = saveInfo.currentWeapon ? saveInfo.currentWeapon.id : null;
        foreach (ItemSO item in saveInfo.playerItens)
        {
            save.currentItemsId.Add(item != null ? item.id : null);
        }
        foreach (int amout in saveInfo.ItensQuantity)
        {
            save.currentItemsQuantity.Add(amout);
        }

        //Write

        Debug.Log(Application.dataPath + "/Save.json");
        var json = JsonUtility.ToJson(save);
        File.WriteAllText(Application.dataPath + "/Save.json", json);

    }
    public static void Load(SaveInfoSO save)
    {
        //Read
        string path = Application.dataPath + "/Save.json";
        SaveFile load = new SaveFile();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            load = JsonUtility.FromJson<SaveFile>(json);
        }
        else
        {
            Debug.LogError("No save found.");
            return;
        }

        //Get player
        save.playerPosition = load.playerPosition;
        save.playerRotation = load.playerRotation;

        //Find itens
        save.currentWeapon = ReferencesSO.instance.items.Find(ItemSO => ItemSO.id == load.currentWeaponId);
        save.playerItens.Clear();
        foreach (string s in load.currentItemsId)
        {
            save.playerItens.Add(ReferencesSO.instance.items.Find(ItemSO => ItemSO.id == s));
        }
        foreach (int s in load.currentItemsQuantity)
        {
            save.ItensQuantity.Add(s);
        }
    }
}