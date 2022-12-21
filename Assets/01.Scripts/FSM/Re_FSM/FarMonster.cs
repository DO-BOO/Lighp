using System.Collections;
using UnityEngine;
using MonsterLove.StateMachine;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading;

public class FarMonster : Character, IHittable
{
    [Header("기획 수치")]
    public float moveRange = 50.0f;

    public float attackCheckRange = 10f; // 공격 사거리

    [Header("AVOID")]
    public float avoidRange = 40.0f; // 회피 가능해진 거리
    public float AVOID_DURATION = Define.AVOID_DURATION; // 회피 시간
    public float AVOID_COOLTIME = Define.AVOID_COOLTIME; // 회피 쿨타임
    public float AVOID_DISTANCE = Define.AVOID_DISTANCE; // 회피하는 거리

    MonsterData monsterData = null;
    private int ID => monsterData.number;

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

    private float attackRange = 30.0f;
    private const float colRadius = 100f;

    private DamageFlash flashEffect;

    protected override void ChildAwake()
    {
        //fsm = new StateMachine<States>(this);
        target = SearchTarget();
        monsterHP = GetComponent<CharacterHp>();
        flashEffect = GetComponent<DamageFlash>();

        if (GameManager.Instance.SpreadData.IsLoading)  // 로딩이 안 된 상태
        {
            EventManager.StartListening(Define.ON_END_READ_DATA, SetMonster);
        }
        else
        {
            SetMonster();
        }
    }

    private void SetMonster()
    {
        monsterData = GameManager.Instance.SpreadData.GetData<MonsterData>(1);
        fsm = StateMachine<States>.Initialize(this, States.Idle);
        ResetMonster();
    }

    private void ResetMonster()
    {
        monsterHP.Hp = monsterData.maxHp;
        agent.speed = monsterData.moveSpeed;
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
    int hashStun = Animator.StringToHash("Stun");
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
        if (distance <= attackRange)
        {
            fsm.ChangeState(States.Attack);
        }
        else if (distance <= moveRange)
        {
            fsm.ChangeState(States.Walk);
        }

    }


    private void Idle_Update()
    {
        CheckDistanceIdle();
    }


    #endregion

    #region WALK

    private void SetMove(bool isMove)
    {
        agent.isStopped = !isMove;
    }

    private void Move()
    {
        if (target == null) return;
        LookTarget(target);
        agent.SetDestination(target.position);
    }

    private void CheckDistanceWalk()
    {
        if (distance <= avoidRange && canAvoid)
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
        SetMove(true);
        AnimationPlay(hashWalk, true);
    }

    private void Walk_Update()
    {
        CheckDistanceWalk();

        Move();
    }

    private void Walk_Exit()
    {
        SetMove(false);
        AnimationPlay(hashWalk, false);
    }

    #endregion

    #region ATTACK

    // 발사체 발사 함수
    // 방향 구하고 발사체 각도 변경
    // PoolManager 사용
    public void Shooting()
    {
        LookTarget(target);
        FarMonsterBullet bullet = BulletInstantiate();
        bullet.gameObject.SetActive(true);
    }
    private FarMonsterBullet BulletInstantiate()
    {
        Vector3 pos = new Vector3(transform.position.x + 2.0f, 1.0f, transform.position.z);
        Transform tDir = SearchTarget();
        Vector3 direction = (tDir.position - pos).normalized;
        direction.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        FarMonsterBullet obj = GameManager.Instance.Pool.Pop("BasicFarMonsterBullet", null, pos, lookRotation) as FarMonsterBullet;
        obj.SetDamage(monsterData.attackPower);
        return obj;
    }

    private void CheckDistanceAttack()
    {
        if (distance < avoidRange && canAvoid)
        {
            fsm.ChangeState(States.Avoid);
        }
        else if (distance > attackRange)
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

    private bool isStun = false;
    private bool IsStun => isStun;

    public bool isEnemy => throw new System.NotImplementedException();

    private float stunTime = 0f;

    private void Stun_Enter()
    {
        SetMove(false);
        StartCoroutine(Stun());
        AnimationPlay(hashStun, true);
    }

    private void Stun_Exit()
    {
        StopCoroutine(Stun());
        isStun = false;
        AnimationPlay(hashStun, false);
    }

    private IEnumerator Stun()
    {
        isStun = true;
        yield return new WaitForSeconds(stunTime);
        fsm.ChangeState(States.Walk);
    }

    #endregion

    #region AVOID

    private bool isAvoid = false;
    private bool canAvoid = true;

    private void Avoid_Enter()
    {
        canAvoid = false;
        isAvoid = true;
        Avoid(-dir);
        Debug.Log("AVOID");
        StartCoroutine(AvoidCoolTime());
    }

    private void Avoid(Vector3 velocity)
    {
        Vector3 addTurnDir = Vector3.zero;
        int randDir = Random.Range(0, 101);
        if (randDir >= 50)
        {
            addTurnDir = new Vector3(0f, 45f, 0f);
        }
        else
        {
            addTurnDir = new Vector3(0f, -45f, 0f);
        }
        transform.Rotate(addTurnDir, Space.Self);
        velocity = (-1 * transform.forward);
        Vector3 destination = transform.position + velocity.normalized * Define.AVOID_DISTANCE;
        if(!Physics.Raycast(transform.position, velocity, Define.AVOID_DISTANCE, blockLayerMask))
        {
            transform.DOKill();
            transform.DOMove(destination, AVOID_DURATION).OnComplete(() => { EndAvoid(); });
        }
        else
        {
            fsm.ChangeState(States.Walk);
        }
    }

    private void EndAvoid()
    {
        fsm.ChangeState(States.Walk);
        rigid.velocity = Vector3.zero;
    }

    private IEnumerator AvoidCoolTime()
    {
        isAvoid = false;
        yield return new WaitForSeconds(AVOID_COOLTIME);
        canAvoid = true;
    }

    #endregion

    #region HIT

    float hitDelay = 1.0f;

    private void Hit_Enter()
    {
        StartCoroutine(NextHitState());
    }

    IEnumerator NextHitState()
    {
        yield return new WaitForSeconds(hitDelay);
        fsm.ChangeState(States.Walk);
    }

    // 데미지 입었을 때 호출 (데미지 입은 상태로 전환)

    public void GetDamage(int damage, float hitStun, bool isCritical, float criticalFactor)
    {
        AnimationPlay(hashHit);
        flashEffect.DamageEffect();
        monsterHP.Hit(damage);
        if (monsterHP.IsDead)
        {
            fsm.ChangeState(States.Die);
            return;
        }

        if (isStun) return;
        if (hitStun>0)
        {
            stunTime = hitStun;
            fsm.ChangeState(States.Stun);
        }
        else
            fsm.ChangeState(States.Hit);
    }

    #endregion

    #region DIE

    private void Die_Enter()
    {
        AnimationPlay(hashDie, true);
        SetMove(false);
        MonsterDie();
    }

    private void MonsterDie()
    {
        gameObject.SetActive(false);
    }

    #endregion

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_END_READ_DATA, SetMonster);
    }

}
