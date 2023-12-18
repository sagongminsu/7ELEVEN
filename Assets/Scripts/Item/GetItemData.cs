using System.Collections.Generic;
using UnityEngine;

public class GetItemData : MonoBehaviour, IInteractable
{
    string itemName;
    Item item;

    void Start()
    {
        itemName = gameObject.name.Replace("(Clone)", "").Trim();
        item = FindItem(itemName, ItemDataManager.Instance.resourceList); // 해당 컴포넌트가 적용된 오브젝트에 해당 오브젝트와 동일한 이름을 사용하는 데이터를 resourceList에서 데이터를 찾아서 할당

        if (item == null)
        {
            item = FindItem(itemName, ItemDataManager.Instance.foodList); // 해당 컴포넌트가 적용된 오브젝트에 해당 오브젝트와 동일한 이름을 사용하는 데이터를 foodList에서 데이터를 찾아서 할당
        }

        if (item == null)
        {
            item = FindItem(itemName, ItemDataManager.Instance.medicinesList);// 해당 컴포넌트가 적용된 오브젝트에 해당 오브젝트와 동일한 이름을 사용하는 데이터를 medicinesList에서 데이터를 찾아서 할당
        }

        if (item == null)
        {
            item = FindItem(itemName, ItemDataManager.Instance.equipmentItemItemList);// 해당 컴포넌트가 적용된 오브젝트에 해당 오브젝트와 동일한 이름을 사용하는 데이터를 equipmentItemItemList에서 데이터를 찾아서 할당
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
        Inventory.instance.AddItem(item);
        return string.Format($"Pickup {item.name}");
    }

    public void OnInteract()
    {
        Destroy(gameObject);
    }
}