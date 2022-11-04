using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class BasicFarMonster : StateMachine
{
    private Transform target = null; // 타겟

    // 상태 스크립트
    public FarMonsterIdle idleState;
    public FarMonsterMove moveState;
    public FarMonsterAttack attackState;
    public FarMonsterDamage damageState;
    public FarMonsterDie dieState;

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
    public float moveRange = 25.0f; // 일정 거리 이상이 되면 쫓아감
    public float attackRange = 25.0f; // 공격 거리 안에 들어오면 공격
    private float colRadius = 100.0f; // 인식 콜라이더 반지름
    private float walkingSpeed = 10.0f; // 쫓아가는 스피드

    // 애니메이션 Hash
    [HideInInspector]
    public int hashWalk = Animator.StringToHash("Walk");
    [HideInInspector]
    public int hashAttack = Animator.StringToHash("Attack");
    [HideInInspector]
    public int hashDamage = Animator.StringToHash("Damage");
    [HideInInspector]
    public int hashDie = Animator.StringToHash("Die");

    // 발사체
    public GameObject bullet;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();

        // 상태 할당
        idleState = new FarMonsterIdle(this);
        moveState = new FarMonsterMove(this);
        attackState = new FarMonsterAttack(this);
        damageState = new FarMonsterDamage(this);
        dieState = new FarMonsterDie(this);

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

    #endregion

    #region TARGET

    // 타겟 찾기
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

    // 타겟 쳐다보는 함수
    public void LookTarget(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        transform.rotation = rot;
    }

    #endregion

    #region Damage

    // 데미지 입었을 때 호출
    // 데미지 입은 상태로 전환
    public void Damaged()
    {
        ChangeState(damageState);
    }

    #endregion

    #region Shoot

    // 발사체 발사 함수
    // 방향 구하고 발사체 각도 변경
    // PoolManager 사용
    public void Shooting()
    {
        Vector3 pos = new Vector3(transform.position.x+2.0f, 1.0f, transform.position.z);
        Transform tDir = SerachTarget();
        Vector3 direction = (tDir.position - pos).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        FarMonsterBullet obj = GameManager.Instance.Pool.Pop("BasicFarMonsterBullet", null, pos, lookRotation) as FarMonsterBullet;
        obj.gameObject.SetActive(true);
    }

    #endregion

    #region ANIMATION

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

    #endregion
}
