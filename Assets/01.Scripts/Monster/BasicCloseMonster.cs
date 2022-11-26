using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// �ٰŸ� Monster�� �⺻ FSM
/// </summary>
public class BasicCloseMonster : MonsterFSM
{
    private Transform target = null; // Ÿ��

    // ���� ��ũ��Ʈ
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
    [HideInInspector]
    public CapsuleCollider collider;

    // �ʿ� ���� => ���߿� SO�� ���� ����
    public float moveRange = 12.0f;
    public float attackRange = 12f;
    private float colRadius = 100.0f;
    private float walkingSpeed = 10.0f;

    private const float MAX_HP = 100f; // ü��
    float HP = MAX_HP; // ü��
    public float GetHP => HP;
    public bool Live => live;
    private bool live = true;

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
        collider = GetComponent<CapsuleCollider>();

        // ���� �Ҵ�
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
        live = true;
        agent.speed = walkingSpeed;
        agent.stoppingDistance = attackRange;
    }

    #endregion

    #region GET
    public float distance => GetDistance(); // Ÿ�ٰ��� �Ÿ�
    public Vector3 dir => GetDirection(); // Ÿ�ٰ��� �Ÿ�
    protected override BaseState GetInitState() { return idleState; } // �⺻ State ��������

    // Ÿ�ٰ��� �Ÿ� ���ϱ�
    protected override float GetDistance()
    {
        return Vector3.Distance(target.transform.position, transform.position);
    }
    
    // Ÿ�ٰ��� ���� ���ϱ�
    protected override Vector3 GetDirection()
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;
        return dir;
    }

    #endregion

    #region TARGET

    // Ÿ�� ���ϱ�
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
    // Ÿ�� �Ĵٺ���
    public void LookTarget(Transform target)
    {
        Vector3 dir = GetDirection();
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        transform.rotation = rot;
    }
    #endregion

    #region DAMAGE

    // ������ �Ծ��� �� ȣ�� (������ ���� ���·� ��ȯ)
    public void Damaged(bool isStun)
    {
        if (!live) return;
        SetHP(false, 20f);
        if (stunning)
        {
            return;
        }
        if (isStun && !isStunCool)
        {
            ChangeState(stunState);
            StartCoroutine(StunCoolTimer());
        }
        else 
            ChangeState(damageState);
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
        if (HP <= 0)
        {
            live = false;
            ChangeState(dieState);
        }
    }
    
    public void ReviveHP()
    {
       HP = MAX_HP;
    }
    #endregion

    #region STUN

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

    #endregion

    #region ANIMATION
    // �ִϸ��̼� Hash
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

    // �̵� �ִϸ��̼�
    public void MoveAnimation(bool isOn)
    {
        anim.SetBool(hashWalk, isOn);
    }

    // ���� �ִϸ��̼�
    public void AttackAnimation(bool isOn)
    {
        anim.SetBool(hashAttack, isOn);
    }

    // ���� �ִϸ��̼�
    public void DieAnimation(bool isOn)
    {
        anim.SetBool(hashDie, isOn);
    }

    // ������ �Դ� �ִϸ��̼�
    public void DamageAnimation()
    {
        anim.SetTrigger(hashDamage);
    }

    // ���� �ִϸ��̼�
    public void StunAnimation(bool isOn)
    {
        anim.SetBool(hashStun, isOn);
    }
    #endregion
}
