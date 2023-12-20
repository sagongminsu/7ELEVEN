using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{
    public float attackRate;
    private bool attacking;
    public float attackDistance;

    public float useStamina;

    [Header("Resource Gathering")]
    public bool doesGatherResources;

    public LayerMask resourceMask;

    [Header("Combat")]
    public bool doesDealDamage;
    public int damage;

    private Animator animator;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        animator = GetComponent<Animator>();
    }

    public override void OnAttackInput(PlayerConditions conditions)
    {
        if (!attacking)
        {
            if (conditions.UseStamina(useStamina))
            {
                attacking = true;
                animator.SetTrigger("Attack");
                Invoke("OnCanAttack", attackRate);
            }
        }
    }

    void OnCanAttack()
    {
        attacking = false;
    }
    bool CompareLayer(int layer, LayerMask layerMask)
    {
        return (layerMask.value & (1 << layer)) != 0;
    }

    public void OnHit()
    {
       

        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackDistance))
        {
            if (doesGatherResources && CompareLayer(hit.collider.gameObject.layer, resourceMask) && hit.collider.TryGetComponent(out Resource resource))
            {
                resource.capacity -= damage;
                resource.Gather(hit.point, hit.normal);
            }
            else
            {
                Debug.Log("매칭되지 않습니다.");
            }

            if (doesDealDamage && CompareLayer(hit.collider.gameObject.layer, resourceMask) && hit.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakePhysicalDamage(damage);
            }
        }
    }

}
