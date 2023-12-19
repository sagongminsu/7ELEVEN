using System.Collections.Generic;
using UnityEngine;

public class GetItemData : MonoBehaviour, IInteractable
{
    string itemName;
    Item item;
    EquipmentItem equipmentItem;

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
            equipmentItem = FindItem(itemName, ItemDataManager.Instance.equipmentItemItemList);// �ش� ������Ʈ�� ����� ������Ʈ�� �ش� ������Ʈ�� ������ �̸��� ����ϴ� �����͸� equipmentItemItemList���� �����͸� ã�Ƽ� �Ҵ�
            Debug.Log(equipmentItem.name);
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
        if (item != null)
        {
            return string.Format($"Pickup {item.name}");
        }
        else if (equipmentItem != null)
        {
            return string.Format($"Pickup {equipmentItem.name}");
        }
        else
        {
            return "������ ���� ���� ";
        }
    }

    public void OnInteract()
    {
        Inventory.instance.AddItem(item);
        Destroy(gameObject);
    }
}