using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ItemSlot
{
    public Item item;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    public AudioSource openCloseSound;
    public AudioClip openSound;

    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform dropPosition;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatNames;
    public TextMeshProUGUI selectedItemStatValues;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    private int curEquipIndex;

    private PlayerController controller;
    private PlayerConditions condition;

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    public static Inventory instance;
    void Awake()
    {
        instance = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerConditions>();
    }
    private void Start()
    {
        inventoryWindow.SetActive(false);
        slots = new ItemSlot[uiSlots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }

        ClearSeletecItemWindow();
    }

    public void OnInventoryButton(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            Toggle();
        }
    }


    public void Toggle()
    {
        if (inventoryWindow.activeInHierarchy)
        {
            inventoryWindow.SetActive(false);
            onCloseInventory?.Invoke();
            controller.ToggleCursor(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory?.Invoke();
            controller.ToggleCursor(true);
            if (openSound != null && openCloseSound != null)
            {
                openCloseSound.clip = openSound;
                openCloseSound.Play();
            }
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem(Item item)
    {
        if (item.canStack)
        {
            ItemSlot slotToStackTo = GetItemStack(item);
            if (slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }

        ThrowItem(item);
    }

    void ThrowItem(Item item)
    {
        Instantiate(item.dropObject, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].quantity <= 0)//갯수가 0 이하인 아이템들은 인벤토리에서 제거
            {
                slots[i].item = null;
            }

            if (slots[i].item != null)
                uiSlots[i].Set(slots[i]);
            else
                uiSlots[i].Clear();
        }
    }

    ItemSlot GetItemStack(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
                return slots[i];
        }

        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }

        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null)
            return;

        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.name;
        selectedItemDescription.text = selectedItem.item.text;

        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;

        if (selectedItem.item.itemType != ItemType.Equipment)
        {
            for (int i = 0; i < selectedItem.item.consumables.Length; i++)
            {
                selectedItemStatNames.text += selectedItem.item.consumables[i].type.ToString() + "\n";
                selectedItemStatValues.text += selectedItem.item.consumables[i].value.ToString() + "\n";
            }
        }
        else
        {
            selectedItemStatValues.text += selectedItem.item.point.ToString() + "\n";
        }

        useButton.SetActive(selectedItem.item.itemType == ItemType.Food || selectedItem.item.itemType == ItemType.Medicines);
        equipButton.SetActive(selectedItem.item.itemType == ItemType.Equipment && !uiSlots[index].equipped);
        unEquipButton.SetActive(selectedItem.item.itemType == ItemType.Equipment && uiSlots[index].equipped);
        dropButton.SetActive(true);
    }

    private void ClearSeletecItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void OnUseButton()
    {
        if (selectedItem.item.itemType == ItemType.Food)
        {
            for (int i = 0; i < selectedItem.item.consumables.Length; i++)
            {
                switch (selectedItem.item.consumables[i].type)
                {
                    case ConsumableType.Health:
                        condition.Heal(selectedItem.item.consumables[i].value); break;
                    case ConsumableType.Hunger:
                        condition.Eat(selectedItem.item.consumables[i].value); break;
                }
            }
        }

        else if (selectedItem.item.itemType == ItemType.Medicines)
        {
            for (int i = 0; i < selectedItem.item.consumables.Length; i++)
            {
                switch (selectedItem.item.consumables[i].type)
                {
                    case ConsumableType.Health:
                        condition.Heal(selectedItem.item.consumables[i].value); break;
                    case ConsumableType.Hunger:
                        condition.Eat(selectedItem.item.consumables[i].value); break;
                }
            }
        }
        RemoveSelectedItem();
    }

    public void OnEquipButton()
    {
        if (uiSlots[curEquipIndex].equipped)
        {
            UnEquip(curEquipIndex);
        }

        uiSlots[selectedItemIndex].equipped = true;
        curEquipIndex = selectedItemIndex;
        EquipManager.instance.EquipNew(selectedItem.item);
        UpdateUI();

        SelectItem(selectedItemIndex);

    }

    void UnEquip(int index)
    {
        uiSlots[index].equipped = false;
        EquipManager.instance.UnEquip();
        UpdateUI();

        if (selectedItemIndex == index)
        {
            SelectItem(index);
        }

    }

    public void OnUnEquipButton()
    {
        UnEquip(selectedItemIndex);
    }

    public void OnDropButton()
    {
        ThrowItem(selectedItem.item);
        RemoveSelectedItem();
    }

    private void RemoveSelectedItem()
    {
        selectedItem.quantity--;

        if (selectedItem.quantity <= 0)
        {
            if (uiSlots[selectedItemIndex].equipped)
            {
                UnEquip(selectedItemIndex);
            }

            selectedItem.item = null;
            ClearSeletecItemWindow();
        }

        UpdateUI();
    }

    public void ConsumptionItem(Item item)
    {
        ItemSlot slot = FindItemSlot(item);

        if (slot != null && slot.quantity > 0)
        {
            slot.quantity--;

            if (slot.quantity <= 0)
            {
                slot.item = null;
            }

            UpdateUI();
        }
    }

    private ItemSlot FindItemSlot(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item)
            {
                return slots[i];
            }
        }

        return null;
    }

    public bool HasItems(Item item, int quantity)
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.item == item)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasItems(int index, int quantity)//아이템 코드로 아이템을 갖고 있는지 체크하는 함수
    {
        foreach (ItemSlot slot in slots)//인벤토리 탐색
        {
            if (slot.item != null)
            {
                if (slot.item.index == index && slot.quantity >= quantity)//필요한 개수와 아이템 코드가 충족될 경우 참 리턴
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void RemoveItem(int index, int quantity)//특정 아이템을 갯수만큼 인벤토리에서 감소
    {
        foreach (ItemSlot slot in slots)//인벤토리 탐색
        {
            if (slot.item != null)
            {
                if (slot.item.index == index)//찾으려는 아이템 코드가 일치하는 경우,
                {
                    slot.quantity -= quantity;//갯수 감소
                    
                    break;
                }
            }
        }
    }
}
