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
        if (context.phase == InputActionPhase.Performed && curEquip != null && playerController.canLook)
        {
            curEquip.OnAttackInput();
        }
    }

    public void EquipNew(Item item)
    {
        UnEquip();
        // curEquip = Instantiate(item.equipPregab, equipParent).GetComponent<Equip>();
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
