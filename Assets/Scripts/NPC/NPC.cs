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
    private SkinnedMeshRenderer[] meshRenderers; // 피격 관련

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
        if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f) // Wandering 상태에서, 다음에 이동해야 하는 거리가 짧을 때 
        {
            SetState(AIState.Idle); // Idle 로 전환.
            Invoke("WanderToNewLocation", Random.Range(minWanderDistance, maxWanderDistance));
        }

        if (playerDistance < detectDistance) // player 가 인지하는 거리 안에 들어왔다
        {
            SetState(AIState.Attacking);
        }
    }

    private void AttackingUpdate()
    {
        if (playerDistance > attackDistance || !IsPlayerInFieldOfView()) // // 공격중일 때 플레이어가 나보다 멀어진다 ?
        {
            agent.isStopped = false;
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(PlayerController.instance.transform.position, path)) // 경로 체크해보고 경로가 가능한 경로면 그 경로로 이동하고 아니면 도망간다
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
            // 실제 공격하는 부분
            agent.isStopped = true;
            if (Time.time - lastAttackTime > attackRate) // 이 시간 안에서 공격 진행
            {
                lastAttackTime = Time.time;
                PlayerController.instance.GetComponent<IDamageable>().TakePhysicalDamage(damage); // 데미지 전달
                animator.speed = 1;
                animator.SetTrigger("Attack");
            }
        }
    }

    private void FleeingUpdate()
    {
        if (agent.remainingDistance < 0.1f) // 이동하는 데가 가까운 경우
        {
            agent.SetDestination(GetFleeLocation());
        }
        else // 경로가 많이 남아있는 경우
        {
            SetState(AIState.Wandering);
        }
    }

    private bool IsPlayerInFieldOfView() // 시야각에 들어오는지 검사
    {
        Vector3 directionToPlayer = PlayerController.instance.transform.position - transform.position; // 플레이어를 바라보는 방향 구하기
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        // 정중앙이 0도. 사용할 때에는 반을 나눠서 사용한다.
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
                    agent.isStopped = true; // isStopped : 멈추는 동작을 하라는 의미.

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

        animator.speed = agent.speed / walkSpeed; // 애니메이션 속도 조절
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
        // 경로 상 가장 가까운 곳을 가지고 온다.
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (Vector3.Distance(transform.position, hit.position) < detectDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere) * Random.Range(minWanderDistance, maxWanderDistance), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break; // 마음에 드는 게 있거나 30번 다 하면 나오도록 !
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
        return Vector3.Angle(transform.position - PlayerController.instance.transform.position, transform.position + targetPos); // 백터 두 개 사이의 각 구하기
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
        
        // 죽는 애니메이션 추가
        animator.SetTrigger("Die");
        animator.speed = 0;
        // 죽는 모션(5초) 후 DestroyObject 함수 실행
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

        yield return new WaitForSeconds(0.1f); // 실제 시간초 기다리기

        for (int x = 0; x < meshRenderers.Length; x++)
        {
            meshRenderers[x].material.color = Color.white;
        }
    }
}
