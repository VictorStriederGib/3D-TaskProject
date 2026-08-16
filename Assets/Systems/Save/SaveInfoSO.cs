using System.Collections.Generic;
using UnityEngine;

public class SaveInfoSO : ScriptableObject
{
    public Vector3 playerPosition; public Quaternion playerRotation;
    public ItemSO currentWeapon;
    public List<ItemSO> playerItens = new List<ItemSO>();
    public List<int> ItensQuantity = new List<int>();
}
public class SaveFile
{
    public Vector3 playerPosition;public Quaternion playerRotation;
    public string currentWeaponId = "";
    public List<string> currentItemsId = new List<string>();
    public List<int> currentItemsQuantity = new List<int>();
}