using System.Collections;
using UnityEngine;
using MonsterLove.StateMachine;
using DG.Tweening;

public class MeleeMonster : Character
{
    MonsterData monsterData = null;
    private int ID => monsterData.number;
    private string name = "";
    private int MAX_HP;
    private int attackPower;
    private float attackCooltime;
    private float dashCooltime;
    private int moveSpeed;
    private int viewDistance;
    private float attackRange = 7.0f;

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
    private const float colRadius = 100f;

    public GameObject dashWarningLine;

    protected override void ChildAwake()
    {
        //fsm = new StateMachine<States>(this);
        monsterHP = GetComponent<CharacterHp>();
        fsm = StateMachine<States>.Initialize(this, States.Idle);
        EventManager.StartListening(Define.ON_END_READ_DATA, SetMonster);

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

    private void SetMonster()
    {
        monsterData = GameManager.Instance.SpreadData.GetData<MonsterData>(0);
        name = monsterData.name;
        MAX_HP = monsterData.maxHp;
        attackPower = monsterData.attackPower;
        attackCooltime = monsterData.pt1Cool;
        dashCooltime = monsterData.pt2Cool;
        moveSpeed = monsterData.moveSpeed;
        viewDistance = monsterData.viewDistance;
        attackRange = monsterData.attackRange;
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
    const float dash_distance = 40f;

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

    #region ATTACK

    private void CheckDistanceAttack()
    {
        if(distance > attackRange)
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

    private bool isDash = false;
    private bool canDash = true;

    private void Dash_Enter()
    {
        canDash = false;
        isDash = true;
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
        yield return new WaitForSeconds(1.0f);
        Vector3 destination = transform.position + velocity.normalized * DASH_DISTANCE;
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
        isDash = false;
        yield return new WaitForSeconds(DASH_COOLTIME);
        canDash = true;
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
    public void Damaged(bool isStun)
    {
        AnimationPlay(hashHit);
        monsterHP.Hit(10);
        if (monsterHP.IsDead)
        {
            fsm.ChangeState(States.Die);
            return;
        }

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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            Damaged(true);
        }
    }

}
