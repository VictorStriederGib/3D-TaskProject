using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Chest : MonoBehaviour
{
    Animator anim;
    [SerializeField] InventorySO chestInventory;
    [SerializeField] InventoryUI ui;
    bool interactable = false, interacting = false;
    public UnityEvent onInteractable, notInteractable, onChestOpen, onChestClose;
    private void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.Space))
        {
            if (interacting)
            {
                CloseChest();
            }
            else
            {
                OpenChest();
            }
        }
    }
    [ContextMenu("Open")]
    void OpenChest()
    {
        onChestOpen.Invoke(); interacting = true;
        ui.LoadInventory(chestInventory);
    }
    [ContextMenu("Close")]
    void CloseChest()
    {
        onChestClose.Invoke(); interacting = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onInteractable.Invoke();
            interactable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            notInteractable.Invoke();
            interactable = false;
            interacting = false;
        }
    }
}