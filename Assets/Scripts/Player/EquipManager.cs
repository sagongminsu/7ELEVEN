using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipManager : MonoBehaviour
{
    public Equip curEquip;
    public Transform equipParent;

    private PlayerController playerController;
    private PlayerConditions playerConditions;

    public static EquipManager instance;

    private void Awake()
    {
        instance = this;
        playerConditions = GetComponent<PlayerConditions>();
        playerController = GetComponent<PlayerController>();
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        Debug.Log("공격");

        if (context.phase == InputActionPhase.Performed && playerController.canLook)
        {
            if (curEquip != null)
            {
                curEquip.OnAttackInput(playerConditions);
            }
            else
            {
                Debug.LogWarning("현재 장착된 아이템이 없습니다.");
                // 아이템이 없을 때의 처리 추가 가능
            }
        }
    }

    public void EquipNew(Item item)
    {
        UnEquip();
        EquipmentItem equipmentItem = ItemDataManager.Instance.equipmentItemItemList.Find(equipItem => equipItem.name == item.name);

        if (equipmentItem != null)
        {
            GameObject equipPrefab = equipmentItem.dropObject;
            curEquip = Instantiate(equipPrefab, equipParent).GetComponent<Equip>();
        }

    }

    public void UnEquip()
    {
        if (curEquip != null)
        {
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}
