using System.Collections.Generic;
using UnityEngine;

public class GetItemData : MonoBehaviour
{
    string itemName;
    Item item;

    void Start()
    {
        itemName = gameObject.name.Replace("(Clone)", "").Trim();
        item = FindItem(itemName, ItemDataManager.Instance.resourceList);
        Debug.Log($"Found item: {item.name}");
        Debug.Log($"Found item: {item.index}");
    }

    Item FindItem(string itemName, List<Item> itemList)
    {
        return itemList.Find(item => item.name == itemName);
    }
}
