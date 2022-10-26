using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicFarMonster : StateMachine
{
    private Transform target = null;
    public float distance => GetDistance();

    public FarMonsterIdle idleState;
    public FarMonsterMove moveState;
    public FarMonsterAttack attackState;
    public FarMonsterDamage damageState;
    public FarMonsterDie dieState;

    public LayerMask targetLayerMask;
    public LayerMask blockLayerMask;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Animator anim;

    public float moveRange = 25.0f; // 일정 거리 이상이 되면 쫓아감
    public float attackRange = 25.0f; // 공격 거리 안에 들어오면 공격
    private float colRadius = 50.0f; // 인식 콜라이더 반지름
    private float walkingSpeed = 10.0f; // 쫓아가는 스피드

    public GameObject bullet;

    [HideInInspector]
    public int hashWalk = Animator.StringToHash("Walk");
    [HideInInspector]
    public int hashAttack = Animator.StringToHash("Attack");
    [HideInInspector]
    public int hashDamage = Animator.StringToHash("Damage");
    [HideInInspector]
    public int hashDie = Animator.StringToHash("Die");

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        idleState = new FarMonsterIdle(this);
        moveState = new FarMonsterMove(this);
        attackState = new FarMonsterAttack(this);
        damageState = new FarMonsterDamage(this);
        dieState = new FarMonsterDie(this);

        agent.speed = walkingSpeed;
    }

    // 기본 State 가져오기
    protected override BaseState GetInitState() { return idleState; }

    // 타겟과의 거리 구하기
    protected override float GetDistance()
    {
        return Vector3.Distance(target.transform.position, transform.position);
    }

    public Transform SerachTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, colRadius, targetLayerMask);
        if (cols.Length > 0)
        {
            target = cols[0].gameObject.transform;
            return target;
        }
        else return null;
    }
}
