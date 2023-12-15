using System.Collections.Generic;
using UnityEngine;

public class GetEquipmentItemData : MonoBehaviour
{
    string itemName;
    EquipmentItem equipmentItem;

    void Start()
    {
        itemName = gameObject.name.Replace("(Clone)", "").Trim();
        equipmentItem = FindItem(itemName, ItemDataManager.Instance.equipmentItemItemList);
        Debug.Log($"Found item: {equipmentItem.name}");
        Debug.Log($"Found item: {equipmentItem.index}");
    }

    EquipmentItem FindItem(string itemName, List<EquipmentItem> itemList)
    {
        return itemList.Find(item => item.name == itemName);
    }
}
