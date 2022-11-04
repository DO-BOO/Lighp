using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 근거리 Monster의 기본 FSM
/// </summary>
public class BasicCloseMonster : StateMachine
{
    private Transform target = null; // 타겟
    public float distance => GetDistance(); // 타겟과의 거리
    public Vector3 dir => GetDirection(); // 타겟과의 거리

    // 상태 스크립트
    public BasicMonsterIdle idleState;
    public BasicMonsterMove moveState;
    public BasicMonsterAttack attackState;
    public BasicMonsterDamage damageState;
    public BasicMonsterDie dieState;

    // Layer
    public LayerMask targetLayerMask;
    public LayerMask blockLayerMask;

    // Component
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Rigidbody rigid;

    // 필요 변수
    public float moveRange = 20.0f;
    public float attackRange = 2.5f;
    private float colRadius = 100.0f;
    private float walkingSpeed = 10.0f;

    // 애니메이션 Hash
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
        rigid = GetComponent<Rigidbody>();

        // 상태 할당
        idleState = new BasicMonsterIdle(this);
        moveState = new BasicMonsterMove(this);
        attackState = new BasicMonsterAttack(this);
        damageState = new BasicMonsterDamage(this);
        dieState = new BasicMonsterDie(this);

        agent.speed = walkingSpeed;
    }

    // 기본 State 가져오기
    protected override BaseState GetInitState() { return idleState; }
    
    // 타겟과의 거리 구하기
    protected override float GetDistance()
    {
        return Vector3.Distance(target.transform.position, transform.position);
    }
    
    // 타겟과의 방향 구하기
    protected override Vector3 GetDirection()
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;
        return dir;
    }
    
    // 타겟 구하기
    public  Transform SerachTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, colRadius, targetLayerMask);
        if (cols.Length > 0)
        {
            target = cols[0].gameObject.transform;
            return target;
        }
        else return null;
    }

    // 이동 애니메이션
    public void MoveAnimation(bool isOn)
    {
        anim.SetBool(hashWalk, isOn);
    }

    // 공격 애니메이션
    public void AttackAnimation(bool isOn)
    {
        anim.SetBool(hashAttack, isOn);
    }

    // 죽음 애니메이션
    public void DieAnimation(bool isOn)
    {
        anim.SetBool(hashDie, isOn);
    }

    // 데미지 입는 애니메이션
    public void DamageAnimation()
    {
        anim.SetTrigger(hashDamage);
    }

    // 타겟 쳐다보기
    public void LookTarget(Transform target)
    {
        Vector3 dir = GetDirection();
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        transform.rotation = rot;
    }

    // 데미지 입었을 때 호출 (데미지 입은 상태로 전환)
    public void Damaged()
    {
        ChangeState(damageState);
    }

    // 충돌처리로 데미지 할거면 이런식으로 하면 됨
    // 태그로 몬스터 종류 판단해서 그 스크립트에 데미지 호출하는 형식임
    // 불편하면 수정하고 말해주세용
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="CloseMonster")
        {
            other.GetComponent<BasicCloseMonster>()?.Damaged();
        }
    }
}
