using System.Collections.Generic;
using UnityEngine;

public class GetItemData : MonoBehaviour, IInteractable
{
    string itemName;
    Item item;

    void Start()
    {
        itemName = gameObject.name.Replace("(Clone)", "").Trim();
        item = FindItem(itemName, ItemDataManager.Instance.resourceList); // �ش� ������Ʈ�� ����� ������Ʈ�� �ش� ������Ʈ�� ������ �̸��� ����ϴ� �����͸� resourceList���� �����͸� ã�Ƽ� �Ҵ�

        if (item == null)
        {
            item = FindItem(itemName, ItemDataManager.Instance.foodList); // �ش� ������Ʈ�� ����� ������Ʈ�� �ش� ������Ʈ�� ������ �̸��� ����ϴ� �����͸� foodList���� �����͸� ã�Ƽ� �Ҵ�
        }

        if (item == null)
        {
            item = FindItem(itemName, ItemDataManager.Instance.medicinesList);// �ش� ������Ʈ�� ����� ������Ʈ�� �ش� ������Ʈ�� ������ �̸��� ����ϴ� �����͸� medicinesList���� �����͸� ã�Ƽ� �Ҵ�
        }

        if (item == null)
        {
            item = FindItem(itemName, ItemDataManager.Instance.equipmentItemItemList);// �ش� ������Ʈ�� ����� ������Ʈ�� �ش� ������Ʈ�� ������ �̸��� ����ϴ� �����͸� equipmentItemItemList���� �����͸� ã�Ƽ� �Ҵ�
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