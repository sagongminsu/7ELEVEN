using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Idle,
    Wandering,
    Attacking,
    Fleeing
}

public class NPC : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public int health;
    public float walkSpeed;
    public float runSpeed;
    public GameObject dropOnDeath;

    [Header("AI")]
    private AIState aiState;
    public float detectDistance;
    public float safeDistance;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    [Header("Combat")]
    public int damage;
    public float attackRate;
    private float lastAttackTime;
    public float attackDistance;

    private float playerDistance;

    public float fieldOfView = 120f;

    private NavMeshAgent agent;
    private Animator animator;
    private SkinnedMeshRenderer[] meshRenderers; // �ǰ� ����

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        SetState(AIState.Wandering);
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        animator.SetBool("Moving", aiState != AIState.Idle);

        switch (aiState)
        {
            case AIState.Idle: PassiveUpdate(); break;
            case AIState.Wandering: PassiveUpdate(); break;
            case AIState.Attacking: AttackingUpdate(); break;
            case AIState.Fleeing: FleeingUpdate(); break;
        }
    }

    private void PassiveUpdate()
    {
        if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f) // Wandering ���¿���, ������ �̵��ؾ� �ϴ� �Ÿ��� ª�� �� 
        {
            SetState(AIState.Idle); // Idle �� ��ȯ.
            Invoke("WanderToNewLocation", Random.Range(minWanderDistance, maxWanderDistance));
        }

        if (playerDistance < detectDistance) // player �� �����ϴ� �Ÿ� �ȿ� ���Դ�
        {
            SetState(AIState.Attacking);
        }
    }

    private void AttackingUpdate()
    {
        if (playerDistance > attackDistance || !IsPlayerInFieldOfView()) // // �������� �� �÷��̾ ������ �־����� ?
        {
            agent.isStopped = false;
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(PlayerController.instance.transform.position, path)) // ��� üũ�غ��� ��ΰ� ������ ��θ� �� ��η� �̵��ϰ� �ƴϸ� ��������
            {
                agent.SetDestination(PlayerController.instance.transform.position);
            }
            else
            {
                SetState(AIState.Fleeing);
            }
        }
        else
        {
            // ���� �����ϴ� �κ�
            agent.isStopped = true;
            if (Time.time - lastAttackTime > attackRate) // �� �ð� �ȿ��� ���� ����
            {
                lastAttackTime = Time.time;
                PlayerController.instance.GetComponent<IDamageable>().TakePhysicalDamage(damage); // ������ ����
                animator.speed = 1;
                animator.SetTrigger("Attack");
            }
        }
    }

    private void FleeingUpdate()
    {
        if (agent.remainingDistance < 0.1f) // �̵��ϴ� ���� ����� ���
        {
            agent.SetDestination(GetFleeLocation());
        }
        else // ��ΰ� ���� �����ִ� ���
        {
            SetState(AIState.Wandering);
        }
    }

    private bool IsPlayerInFieldOfView() // �þ߰��� �������� �˻�
    {
        Vector3 directionToPlayer = PlayerController.instance.transform.position - transform.position; // �÷��̾ �ٶ󺸴� ���� ���ϱ�
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        // ���߾��� 0��. ����� ������ ���� ������ ����Ѵ�.
        return angle < fieldOfView * 0.5f;
    }

    private void SetState(AIState newState)
    {
        aiState = newState;
        switch (aiState)
        {
            case AIState.Idle:
                {
                    agent.speed = walkSpeed;
                    agent.isStopped = true; // isStopped : ���ߴ� ������ �϶�� �ǹ�.

                }
                break;
            case AIState.Wandering:
                {
                    agent.speed = walkSpeed;
                    agent.isStopped = false;
                }
                break;
            case AIState.Attacking:
                {
                    agent.speed = runSpeed;
                    agent.isStopped = false;
                }
                break;
            case AIState.Fleeing:
                {
                    agent.speed = runSpeed;
                    agent.isStopped = false;
                }
                break;

        }

        animator.speed = agent.speed / walkSpeed; // �ִϸ��̼� �ӵ� ����
    }

    private void WanderToNewLocation()
    {
        if (aiState != AIState.Idle)
        {
            return;
        }
        SetState(AIState.Wandering);
        agent.SetDestination(GetWanderLocation());
    }

    private Vector3 GetWanderLocation()
    {
        NavMeshHit hit;
        // ��� �� ���� ����� ���� ������ �´�.
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (Vector3.Distance(transform.position, hit.position) < detectDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere) * Random.Range(minWanderDistance, maxWanderDistance), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break; // ������ ��� �� �ְų� 30�� �� �ϸ� �������� !
        }

        return hit.position;
    }

    private Vector3 GetFleeLocation()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (GetDestinationAngle(hit.position) > 90 || playerDistance < safeDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }

    private float GetDestinationAngle(Vector3 targetPos)
    {
        return Vector3.Angle(transform.position - PlayerController.instance.transform.position, transform.position + targetPos); // ���� �� �� ������ �� ���ϱ�
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
        StartCoroutine(DamageFlash());
    }

    private void Die()
    {
        Instantiate(dropOnDeath, transform.position + Vector3.up * 2, Quaternion.identity);
        
        // �״� �ִϸ��̼� �߰�
        animator.SetTrigger("Die");
        animator.speed = 0;
        // �״� ���(5��) �� DestroyObject �Լ� ����
        Invoke("DestroyObject", 5);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private IEnumerator DamageFlash()
    {
        for (int x = 0; x < meshRenderers.Length; x++)
        {
            meshRenderers[x].material.color = new Color(1.0f, 0.6f, 0.6f);
        }

        yield return new WaitForSeconds(0.1f); // ���� �ð��� ��ٸ���

        for (int x = 0; x < meshRenderers.Length; x++)
        {
            meshRenderers[x].material.color = Color.white;
        }
    }
}
