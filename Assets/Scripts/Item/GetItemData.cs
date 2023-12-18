using System.Collections.Generic;
using UnityEngine;

public class GetItemData : MonoBehaviour
{
    string itemName;
    Item item;
    EquipmentItem equipmentItem;

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

        equipmentItem = FindItem(itemName, ItemDataManager.Instance.equipmentItemItemList);

        if (equipmentItem != null)
        {
            Debug.Log($"Found item: {equipmentItem.name}");
            Debug.Log($"Found item: {equipmentItem.index}");
        }

        if (item != null)
        {
            Debug.Log($"Found item: {item.name}");
            Debug.Log($"Found item: {item.index}");
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
}