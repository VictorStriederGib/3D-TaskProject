using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemSO currentItem;
    [SerializeField] Image dragSlot, itemIcon; public Text quantityText;
    public int currentQuantity = 0;
    public ItemType itemType;
    public UnityAction OnUpdateItem = delegate { };
    InventorySO SO;
    int index;
    private void Start()
    {
        TooltipUI.Instance.onDrag += (item, slot) => CheckDragDrop(item, true);
        TooltipUI.Instance.OnDrop += (item, slot) => CheckDragDrop(item, false);
        if (GetComponentInParent<InventoryUI>())
        {
            SO = GetComponentInParent<InventoryUI>().currentInventory;
            index = GetComponentInParent<InventoryUI>().slots.FindIndex(slot => slot == this);
        }
    }
    void Update()
    {
        if (pointerIn && Input.GetMouseButtonDown(1))
        {
            UseItem();
        }
    }
    private void OnDisable()
    {
        OnUpdateItem = delegate { };
    }
    public void UseItem()
    {
        if (SO.slots[index].item != null)
        {
            currentItem.Use(gameObject, SO);
            UpdateItem(currentItem, currentQuantity);
        }
    }
    public void LoadItem(ItemSO item, int quantity)
    {
        currentItem = item;
        itemIcon.enabled = currentItem != null;
        quantityText.enabled = currentItem != null;
        currentQuantity = quantity;
    }
    public void UpdateItem(ItemSO item, int quantity)
    {
        currentItem = item;
        currentQuantity = quantity;
        itemIcon.enabled = currentItem != null;
        quantityText.enabled = currentItem != null;

        if (currentItem)
        {
            itemIcon.sprite = currentItem.icon;
            quantityText.text = currentQuantity.ToString();
        }
        if (currentQuantity <= 0)
        {
            currentQuantity = 0;
            ClearSlot();
        }

        OnUpdateItem.Invoke();
    }
    public void ClearSlot()
    {
        currentItem = null;
        currentQuantity = 0;
        itemIcon.enabled = false;
        quantityText.enabled = false;
    }
    #region Pointers & DragDrop
    bool pointerIn = false;

    void CheckDragDrop(ItemSO item, bool startDrag)
    {
        if (startDrag)
        {
            if (currentItem == item || currentItem == null)
            {
                dragSlot.gameObject.SetActive(true);
            }
            if (currentItem != item && currentItem != null)
            {
                dragSlot.gameObject.SetActive(false);
            }
        }
        else
        {
            dragSlot.gameObject.SetActive(false);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) return;
        if (currentItem == null) return;
        TooltipUI.Instance.onDrag.Invoke(currentItem, this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipUI.Instance.currentSlot = this;
        pointerIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerIn = false;
        if (TooltipUI.Instance.currentSlot == this)
        {
            TooltipUI.Instance.currentSlot = null;
        }
    }
    #endregion
}