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

    // 상태 스크립트
    public BasicMonsterIdle idleState;
    public BasicMonsterMove moveState;
    public BasicMonsterStun stunState;
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

    // 필요 변수 => 나중에 SO로 뽑을 예정
    public float moveRange = 12.0f;
    public float attackRange = 12f;
    private float colRadius = 100.0f;
    private float walkingSpeed = 10.0f;

    private const float MAX_HP = 100f; // 체력
    float HP = MAX_HP; // 체력
    public float GetHP => HP;

    private bool stunning = false;
    public bool IsStun => stunning;
    public void SetStun(bool stop)
    {
        stunning = stop;
    }


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
        stunState = new BasicMonsterStun(this);

        SetMonsterInform();
    }

    #region SET

    public void SetMonsterInform()
    {
        agent.speed = walkingSpeed;
        agent.stoppingDistance = attackRange;
    }

    #endregion

    #region GET
    public float distance => GetDistance(); // 타겟과의 거리
    public Vector3 dir => GetDirection(); // 타겟과의 거리
    protected override BaseState GetInitState() { return idleState; } // 기본 State 가져오기

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

    #endregion

    #region TARGET

    // 타겟 구하기
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
    // 타겟 쳐다보기
    public void LookTarget(Transform target)
    {
        Vector3 dir = GetDirection();
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        transform.rotation = rot;
    }
    #endregion

    #region DAMAGE

    // 데미지 입었을 때 호출 (데미지 입은 상태로 전환)
    public void Damaged(bool isStun)
    {
        if (!stunning && isStun)
        {
            ChangeState(stunState);
        }
        else ChangeState(damageState);
    }

    public void SetHP(bool isHeal, float plusHP)
    {
        if (isHeal)
        {
            HP += plusHP;
        }
        else
        {
            HP -= plusHP;
        }
    }
    
    public void ReviveHP()
    {
       HP = MAX_HP;
    }


    #endregion

    #region ANIMATION

    // 애니메이션 Hash
    [HideInInspector]
    public int hashWalk = Animator.StringToHash("Walk");
    [HideInInspector]
    public int hashAttack = Animator.StringToHash("Attack");
    [HideInInspector]
    public int hashDamage = Animator.StringToHash("Damage");
    [HideInInspector]
    public int hashDie = Animator.StringToHash("Die");
    [HideInInspector]
    public int hashStun = Animator.StringToHash("Stun");

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

    // 스턴 애니메이션
    public void StunAnimation(bool isOn)
    {
        anim.SetBool(hashStun, isOn);
    }
    #endregion

}
