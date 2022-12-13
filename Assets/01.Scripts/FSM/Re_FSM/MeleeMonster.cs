using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using DG.Tweening;
using System.Net.Mime;

public class MeleeMonster : Character, IHittable
{
    MonsterData monsterData = null;
    private int ID => monsterData.number;

    public enum States
    {
        Idle,
        Walk,
        Attack,
        Stun,
        Dash,
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
    private float attackRange = 8.5f;
    private float colRadius = 100f;
    const float dash_distance = 40f;

    public GameObject dashWarningLine;
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

        EventManager.StartListening(Define.ON_START_DARK, StartDark);
        EventManager.StartListening(Define.ON_END_DARK, EndDark);
    }

    private void ResetMonster()
    {
        monsterHP.Hp = monsterData.maxHp;
        agent.speed = monsterData.moveSpeed;
        agent.stoppingDistance = attackRange;
        //colRadius = monsterData.viewDistance;
        colRadius = 100f;
    }

    private void SetMonster()
    {
        monsterData = GameManager.Instance.SpreadData.GetData<MonsterData>(0);
        fsm = StateMachine<States>.Initialize(this, States.Idle);
        ResetMonster();
    }

    #region GET

    public float distance => GetDistance();
    public Vector3 dir => GetDirection();

    // 타겟과의 거리 구하기
    private float GetDistance()
    {
        if (target == null)
            return 0f;

        return Vector3.Distance(transform.position, target.position);
    }

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
        // transform.Rotate(dir.normalized);
        transform.rotation = rot;
    }


    #endregion

    #region ANIMATION

    int hashAttack = Animator.StringToHash("IsAttack");
    int hashWalk = Animator.StringToHash("Walk");
    int hashHit = Animator.StringToHash("Damage");
    int hashStun = Animator.StringToHash("Stun");
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
        if (distance <= monsterData.attackRange)
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
        if (distance <= attackRange)
        {
            fsm.ChangeState(States.Attack);
        }
        else if (distance >= dash_distance && canDash)
        {
            fsm.ChangeState(States.Dash);
        }
    }

    private void Walk_Enter()
    {
        AnimationPlay(hashWalk, true);
        SetMove(true);
    }

    private void Walk_Update()
    {
        CheckDistanceWalk();

        Move();
    }

    private void Walk_Exit()
    {
        AnimationPlay(hashWalk, false);
        SetMove(false);
    }

    #endregion

    #region ATTACK

    private void CheckDistanceAttack()
    {
        if (distance > attackRange)
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

    public void Attack()
    {
        if (target != null)
        {
            if (distance <= attackRange + 1.0f)
            {
                LookTarget(target);

                target.GetComponent<CharacterHp>()?.Hit(monsterData.attackPower);
            }
        }
    }

    #endregion

    #region STUN

    private bool isStun = false;
    private bool IsStun => isStun;

    public bool isEnemy => throw new System.NotImplementedException();

    private float stunTime = 0f;

    private void Stun_Enter()
    {
        AnimationPlay(hashStun, true);
        SetMove(false);
        StartCoroutine(Stun());
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

    #region DASH

    private const float DASH_DURATION = Define.DASH_DURATION;
    private const float DASH_DISTANCE = Define.DASH_DISTANCE;
    private float DASH_COOLTIME = Define.DASH_COOLTIME;

    private bool isDash = false;
    private bool canDash = true;

    private void Dash_Enter()
    {
        canDash = false;
        isDash = true;
        agent.velocity = Vector3.zero;
        rigid.velocity = Vector3.zero;
        WarningDash(dir);
        StartCoroutine(Dash(dir));
        StartCoroutine(DashCoolTime());
    }

    public void WarningDash(Vector3 _end)
    {
        Vector3 newPos = transform.position;
        //WarningLine line = GameManager.Instance.Pool.Pop("WarningLine", null, newPos, Quaternion.identity) as WarningLine;
        WarningLine line = Instantiate(dashWarningLine, newPos, Quaternion.identity).GetComponent<WarningLine>();
        line.SetPos(_end);
    }

    private IEnumerator Dash(Vector3 velocity)
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(1.0f);
        Vector3 destination = transform.position + velocity.normalized * DASH_DISTANCE;
        transform.DOKill();
        transform.DOMove(destination, DASH_DURATION).OnComplete(() => { EndDash(); });
    }

    private void EndDash()
    {
        agent.isStopped = false;
        fsm.ChangeState(States.Walk);
        rigid.velocity = Vector3.zero;
    }

    private IEnumerator DashCoolTime()
    {
        isDash = false;
        yield return new WaitForSeconds(DASH_COOLTIME);
        canDash = true;
    }

    #endregion

    #region HIT

    float hitDelay = 1.0f;

    private void Hit_Enter()
    {
        AnimationPlay(hashHit);
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
        flashEffect.DamageEffect();
        monsterHP.Hit(damage);
        if (monsterHP.IsDead)
        {
            fsm.ChangeState(States.Die);
            return;
        }

        if (isStun) return;
        if (hitStun > 0)
        {
            stunTime = hitStun;
            fsm.ChangeState(States.Stun);
        }
        else
        {
            fsm.ChangeState(States.Hit);
        }
    }

    #endregion

    #region DIE

    private void Die_Enter()
    {
        SetMove(false);
        AnimationPlay(hashDie, true);
        MonsterDie();
    }

    private void MonsterDie()
    {
        gameObject.SetActive(false);
    }

    #endregion

    private void StartDark()
    {
        agent.speed *= Define.DARK_SUB_ENEMY_SPEED_WEIGHT;
        animator.SetFloat("Speed", Define.DARK_SUB_ENEMY_SPEED_WEIGHT); ;
    }

    private void EndDark()
    {
        agent.speed *= 1 / Define.DARK_SUB_ENEMY_SPEED_WEIGHT;
        animator.SetFloat("Speed", 1f); ;
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_END_READ_DATA, SetMonster);
        EventManager.StopListening(Define.ON_START_DARK, StartDark);
        EventManager.StopListening(Define.ON_END_DARK, EndDark);
    }
}
