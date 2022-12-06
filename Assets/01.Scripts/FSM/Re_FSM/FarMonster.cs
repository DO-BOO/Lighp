using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MonsterLove.StateMachine;

public class FarMonster : Character
{
    public enum States
    {
        Idle,
        Walk,
        Attack,
        Stun,
        Avoid,
        Hit,
        Die
    }

    StateMachine<States> fsm;

    private Transform target = null; // 타겟

    private CharacterHp monsterHP; // HP

    // Layer
    public LayerMask targetLayerMask;
    public LayerMask blockLayerMask;

    private float moveRange = 50.0f;
    private float attackRange = 40.0f;
    private float moveSpeed = 10.0f;
    private const float colRadius = 100f;

    public GameObject dashWarningLine;

    protected override void ChildAwake()
    {
        //fsm = new StateMachine<States>(this);
        monsterHP = GetComponent<CharacterHp>();
        fsm = StateMachine<States>.Initialize(this, States.Idle);

        target = SearchTarget();
        ResetMonster();
        fsm.ChangeState(States.Idle);
    }

    private void ResetMonster()
    {
        monsterHP.Hp = monsterHP.MaxHp;
        agent.speed = moveSpeed;
        agent.stoppingDistance = attackRange;
    }

    #region GET

    public float distance => GetDistance();
    public Vector3 dir => GetDirection();

    // 타겟과의 거리 구하기
    private float GetDistance() { return Vector3.Distance(target.transform.position, transform.position); }

    // 타겟과의 방향 구하기
    private Vector3 GetDirection()
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;
        return dir;
    }

    #endregion

    #region TARGET

    // 타겟 구하기
    public Transform SearchTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, colRadius, targetLayerMask);
        if (cols.Length > 0)
        {
            return cols[0].gameObject.transform;
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

    #region ANIMATION

    int hashAttack = Animator.StringToHash("Attack");
    int hashWalk = Animator.StringToHash("Walk");
    int hashHit = Animator.StringToHash("Damage");
    int hashDie = Animator.StringToHash("Die");

    public void AnimationPlay(int hash, bool isOn)
    {
        anim.SetBool(hash, isOn);
    }

    public void AnimationPlay(int hash)
    {
        anim.SetTrigger(hash);
    }

    #endregion

    #region IDLE

    private void CheckDistanceIdle()
    {
        if(distance <= attackRange)
        {
            fsm.ChangeState(States.Attack);
        }
        else if (distance <= moveRange)
        {
            fsm.ChangeState(States.Walk);
        }
    }


    private void Idle_Enter()
    {
    }

    private void Idle_Update()
    {
        CheckDistanceIdle();
    }

    private void Idle_Exit()
    {

    }

    #endregion

    #region WALK

    const float monsterSpeed = 10.0f;
    const float avoid_distance = 10f;

    private void Move()
    {
        if (target == null) return;
        LookTarget(target);
        agent.SetDestination(target.position);
    }

    private void CheckDistanceWalk()
    {
        if (distance < avoid_distance && canAvoid)
        {
            fsm.ChangeState(States.Avoid);
        }
        else if (distance <= attackRange)
        {
            fsm.ChangeState(States.Attack);
        }
        
    }

    private void Walk_Enter()
    {
        AnimationPlay(hashWalk, true);
    }

    private void Walk_Update()
    {
        CheckDistanceWalk();

        Move();
    }

    private void Walk_Exit()
    {
        AnimationPlay(hashWalk, false);
    }

    #endregion

    #region Shoot

    // 발사체 발사 함수
    // 방향 구하고 발사체 각도 변경
    // PoolManager 사용
    public void Shooting()
    {
        Vector3 pos = new Vector3(transform.position.x + 2.0f, 1.0f, transform.position.z);
        Transform tDir = SearchTarget();
        Vector3 direction = (tDir.position - pos).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        FarMonsterBullet obj = GameManager.Instance.Pool.Pop("BasicFarMonsterBullet", null, pos, lookRotation) as FarMonsterBullet;
        obj.gameObject.SetActive(true);
    }

    #endregion

    #region ATTACK

    private void CheckDistanceAttack()
    {
        if(distance < avoid_distance && canAvoid)
        {
            fsm.ChangeState(States.Avoid);
        }
        else if(distance > attackRange)
        {
            fsm.ChangeState(States.Walk);
        }
    }

    private void Attack_Enter()
    {
        AnimationPlay(hashAttack, true);
    }

    private void Attack_Update()
    {
        CheckDistanceAttack();
    }
    private void Attack_Exit()
    {
        AnimationPlay(hashAttack, false);
    }

    #endregion

    #region STUN

    private bool stunning = false;
    public bool IsStun => stunning;
    private float coolTime = 10f;
    private bool isStunCool = false;

    private IEnumerator StunCoolTimer()
    {
        isStunCool = true;
        yield return new WaitForSeconds(coolTime);
        StopStunCoolTime();
    }
    private void StopStunCoolTime()
    {
        StopCoroutine(StunCoolTimer());
        isStunCool = false;
    }
    public void SetStun(bool stop)
    {
        stunning = stop;
    }

    private void Stun_Enter()
    {
        Debug.Log("Enter");
    }

    #endregion

    #region DASH

    private const float DASH_DURATION = Define.DASH_DURATION;
    private const float DASH_DISTANCE = Define.DASH_DISTANCE;
    private float DASH_COOLTIME = Define.DASH_COOLTIME;

    private bool canAvoid = true;

    private void Avoid_Enter()
    {
        canAvoid = false;
        WarningDash(-dir);
        StartCoroutine(Avoid(-dir));
        StartCoroutine(DashCoolTime());
    }

    public void WarningDash(Vector3 _end)
    {
        Vector3 newPos = transform.position;
        //WarningLine line = GameManager.Instance.Pool.Pop("WarningLine", null, newPos, Quaternion.identity) as WarningLine;
        WarningLine line = Instantiate(dashWarningLine, newPos, Quaternion.identity).GetComponent<WarningLine>();
        line.SetPos(_end);
    }

    private IEnumerator Avoid(Vector3 velocity)
    {
        yield return new WaitForSeconds(1.0f);
        Vector3 destination = -(transform.position + velocity.normalized * DASH_DISTANCE);
        transform.DOKill();
        transform.DOMove(destination, DASH_DURATION).OnComplete(() => { EndDash(); });
    }

    private void EndDash()
    {
        fsm.ChangeState(States.Walk);
        rigid.velocity = Vector3.zero;
    }

    private IEnumerator DashCoolTime()
    {
        yield return new WaitForSeconds(DASH_COOLTIME);
        canAvoid = true;
    }

    #endregion

    #region HIT

    int damage = 10;

    private void Hit_Enter()
    {
        Debug.Log("Hit");
        AnimationPlay(hashHit);
        monsterHP.Hit(damage);
        if (monsterHP.IsDead) fsm.ChangeState(States.Die);
    }

    // 데미지 입었을 때 호출 (데미지 입은 상태로 전환)
    public void Damaged(bool isStun)
    {
        if (stunning) return;
        if (isStun && !isStunCool)
        {
            fsm.ChangeState(States.Stun);
            StartCoroutine(StunCoolTimer());
        }
        else
            fsm.ChangeState(States.Hit);
    }

    #endregion

    #region DIE

    private void Die_Enter()
    {
        Debug.Log("Die");
        AnimationPlay(hashDie, true);
        MonsterDie();
    }

    private void MonsterDie()
    {
        gameObject.SetActive(false);
    }

    #endregion
}
