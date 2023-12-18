using System.Collections.Generic;
using UnityEngine;

public class GetItemData : MonoBehaviour, IInteractable
{
    string itemName;
    Item item;

    void Start()
    {
        itemName = gameObject.name.Replace("(Clone)", "").Trim();
        item = FindItem(itemName, ItemDataManager.Instance.resourceList);

        if (item == null)
        {
            item = FindItem(itemName, ItemDataManager.Instance.foodList);
        }

        if (item == null)
        {
            item = FindItem(itemName, ItemDataManager.Instance.medicinesList);
        }

        if (item == null)
        {
            item = FindItem(itemName, ItemDataManager.Instance.equipmentItemItemList);
        }
        
    }

    Item FindItem(string itemName, List<Item> itemList)
    {
        return itemList.Find(item => item.name == itemName);
    }

    EquipmentItem FindItem(string itemName, List<EquipmentItem> itemList)
    {
        return itemList.Find(item => item.name == itemName);
    }

    public string GetInteractPrompt()
    {
        return string.Format($"Pickup {item.name}");
    }

    public void OnInteract()
    {
        Destroy(gameObject);
    }
}