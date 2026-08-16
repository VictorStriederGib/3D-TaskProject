using System.Collections.Generic;
using UnityEngine;


public class ReferencesSO : ScriptableObject
{
    public static ReferencesSO instance;
    public List<ItemSO> items = new List<ItemSO>();
    public List<InventorySO> inventories = new List<InventorySO>();
    private void OnValidate()
    {
        if (instance != null)
        {
            instance = this;
        }
    }
    [ContextMenu("Intance this")]
    void BecomeIntance()
    {
        instance = this;
    }
}