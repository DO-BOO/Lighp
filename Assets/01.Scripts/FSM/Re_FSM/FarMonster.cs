using System.Collections;
using UnityEngine;
using MonsterLove.StateMachine;
using DG.Tweening;
using System.Collections.Generic;

public class FarMonster : Character
{
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

    private Transform target = null; // Ÿ��

    private CharacterHp monsterHP; // HP

    // Layer
    public LayerMask targetLayerMask;
    public LayerMask blockLayerMask;

    private float moveRange = 30.0f;
    private float attackRange = 50.0f;
    private float avoidRange = 20.0f;
    private float moveSpeed = 10.0f;
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

    private void SetMonster()
    {
        monsterData = GameManager.Instance.SpreadData.GetData<MonsterData>(1);

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

    // Ÿ�ٰ��� �Ÿ� ���ϱ�
    private float GetDistance() { return Vector3.Distance(target.transform.position, transform.position); }

    // Ÿ�ٰ��� ���� ���ϱ�
    private Vector3 GetDirection()
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;
        return dir;
    }

    #endregion

    #region TARGET

    // Ÿ�� ���ϱ�
    public Transform SearchTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, colRadius, targetLayerMask);
        if (cols.Length > 0)
        {
            return cols[0].gameObject.transform;
        }
        else return null;
    }

    // Ÿ�� �Ĵٺ���
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
        if (distance > moveRange)
        {
            fsm.ChangeState(States.Walk);
        }
        else if (distance <= attackRange)
        {
            fsm.ChangeState(States.Attack);
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


    private void Move()
    {
        if (target == null) return;
        LookTarget(target);
        agent.SetDestination(target.position);
    }

    private void CheckDistanceWalk()
    {
        if (distance < avoidRange && canAvoid)
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

    #region ATTACK

    // �߻�ü �߻� �Լ�
    // ���� ���ϰ� �߻�ü ���� ����
    // PoolManager ���
    public void Shooting()
    {
        Vector3 pos = new Vector3(transform.position.x + 2.0f, 1.0f, transform.position.z);
        Transform tDir = SearchTarget();
        Vector3 direction = (tDir.position - pos).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        FarMonsterBullet obj = GameManager.Instance.Pool.Pop("BasicFarMonsterBullet", null, pos, lookRotation) as FarMonsterBullet;
        obj.gameObject.SetActive(true);
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

    #region AVOID

    private const float AVOID_DURATION = Define.AVOID_DURATION;
    private const float AVOID_DISTANCE = Define.AVOID_DISTANCE;
    private float AVOID_COOLTIME = Define.AVOID_COOLTIME;

    private bool isAvoid = false;
    private bool canAvoid = true;

    private void Avoid_Enter()
    {
        canAvoid = false;
        isAvoid = true;
        Avoid(-dir);
        StartCoroutine(AvoidCoolTime());
    }

    private void Avoid(Vector3 velocity)
    {
        Vector3 destination = transform.position + velocity.normalized * AVOID_DISTANCE;
        transform.DOKill();
        transform.DOMove(destination, AVOID_DURATION).OnComplete(() => { EndAvoid(); });
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

    int damage = 10;
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

    // ������ �Ծ��� �� ȣ�� (������ ���� ���·� ��ȯ)
    public void Damaged(bool isStun)
    {
        AnimationPlay(hashHit);
        monsterHP.Hit(damage);
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
        if(collision.collider.tag=="Player")
        {
            Damaged(false);
        }
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_END_READ_DATA, SetMonster);
    }
}
