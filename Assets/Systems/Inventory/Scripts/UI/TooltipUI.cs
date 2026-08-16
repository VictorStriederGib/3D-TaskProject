using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour, IPointerMoveHandler
{
    public static TooltipUI Instance { get; private set; }
    public ItemSO dragItem;
    public InventorySlotUI firstSlot, currentSlot;
    int firstSlotQuantity = 1;
    public int draggingQuantity = 1;

    public UnityAction<ItemSO, InventorySlotUI> onDrag, OnDrop;
    [Header("UI Elements")]
    public GameObject root;
    public Image itemIcon;
    public Text nameText;
    public Text descriptionText;
    public Text quantityText;

    void Awake()
    {
        Instance = this;
        Hide();
    }
    private void Start()
    {
        onDrag += Drag;
        OnDrop += Drop;
    }
    void Update()
    {
        if (root != null && root.activeSelf)
        {
            Vector2 mousePosition = Input.mousePosition;
            Follow(mousePosition);
        }

        if (dragItem && (int)Input.mouseScrollDelta.y != 0) DragMoreLess((int)Input.mouseScrollDelta.y, firstSlot.currentItem);
        if (Input.GetMouseButtonUp(0) && dragItem != null)
        {
            OnDrop?.Invoke(dragItem, currentSlot);
        }
    }
    public void Drag(ItemSO item, InventorySlotUI slot)
    {
        if (item == null) return;
        firstSlot = slot;
        dragItem = item;

        firstSlotQuantity = firstSlot.currentQuantity;
        draggingQuantity = 1;

        Show(item, Input.mousePosition);
    }
    void DragMoreLess(int amount, ItemSO item)
    {
        draggingQuantity += amount;
        if (draggingQuantity < 1)
        {
            draggingQuantity = 1;

        }
        if (draggingQuantity >= firstSlotQuantity)
        {
            draggingQuantity = firstSlotQuantity;
        }

        UpdateQuantity(item);
    }
    void Drop(ItemSO item, InventorySlotUI slot)
    {
        if (dragItem == null) return;
        
        //Check slot, current item to slot compatibility
        if ((slot == null)
        || (slot.currentItem != null && slot.currentItem != dragItem)
        || slot.itemType != ItemType.Generic && slot.itemType != item.itemType
        )
        {
            //Return item to first slot if not dropped on a valid slot
            firstSlot.currentQuantity += draggingQuantity;
            firstSlot.UpdateItem(dragItem, firstSlot.currentQuantity);
        }
        else
        {
            //Apply the changes to the current slot if dropped on a valid slot, empty or same item
            if (slot != null && (slot.currentItem == null || slot.currentItem == dragItem))
            {
            slot.currentItem = dragItem;
            slot.currentQuantity += draggingQuantity;

            /*if (firstSlot != slot)
            firstSlot.currentQuantity -= draggingQuantity;*/

            firstSlot.UpdateItem(dragItem, firstSlot.currentQuantity);
            slot.UpdateItem(dragItem, slot.currentQuantity);
            }
        }

        

        dragItem = null;
        Hide();
    }

    public void Show(ItemSO item, Vector2 position, int quantityOverride = -1)
    {
        if (item == null)
        {
            Hide();
            return;
        }

        itemIcon.sprite = item.icon;

        if (root != null)
        {
            root.SetActive(true);
        }

        if (nameText != null) nameText.text = item.itemName;
        if (descriptionText != null) descriptionText.text = item.description;
        if (quantityText != null && draggingQuantity > 1)
        {
            int displayQuantity = quantityOverride >= 0 ? quantityOverride : draggingQuantity;
            bool showQuantity = item.stackable && displayQuantity > 1;
            quantityText.gameObject.SetActive(showQuantity);
            quantityText.text = showQuantity ? displayQuantity.ToString() : string.Empty;
        }
        else
        {
            quantityText.gameObject.SetActive(false);
        }

        Follow(position);
        UpdateQuantity(item);
    }

    void UpdateQuantity(ItemSO item)
    {
        quantityText.gameObject.SetActive(draggingQuantity > 1);
        quantityText.text = draggingQuantity.ToString();
        firstSlot.currentQuantity = firstSlotQuantity - draggingQuantity;
        firstSlot.UpdateItem(firstSlot.currentItem, firstSlot.currentQuantity);
    }

    public void Follow(Vector2 position)
    {
        if (root == null) return;

        var rect = root.GetComponent<RectTransform>();
        if (rect != null)
        {
            rect.position = position + new Vector2(24f, -24f);
        }
    }

    public void Hide()
    {
        if (root != null) root.SetActive(false);

        firstSlot = null;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (firstSlot == null) return;
        Show(dragItem, eventData.position);
    }
}