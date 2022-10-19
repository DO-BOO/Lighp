using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Monster의 기본 FSM
/// </summary>
public class BasicMonster : StateMachine
{
    private GameObject target = null;
    public GameObject Target => target;
    public float distance => GetDistance();

    [HideInInspector]
    public BasicMonsterIdle idleState;
    public BasicMonsterMove moveState;
    public BasicMonsterAttack attackState;

    public LayerMask targetLayerMask;
    public LayerMask blockLayerMask;
    public NavMeshAgent agent;
    public Animator anim;

    public float idleRange = 15.0f;
    private float colRadius = 5.0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        idleState = new BasicMonsterIdle(this);
        moveState = new BasicMonsterMove(this);
        attackState = new BasicMonsterAttack(this);
    }

    // 기본 State 가져오기
    protected override BaseState GetInitState() { return idleState; }
    
    // 타겟과의 거리 구하기
    protected override float GetDistance()
    {
        if(Target == null) return -1f;
        return Vector3.Distance(target.transform.position, transform.position);
    }
    
    // 타겟 가져오기
    protected override void SearchTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, colRadius, targetLayerMask);

        if (cols.Length > 0) target = cols[0].gameObject;
        else target = null;
    }

}
