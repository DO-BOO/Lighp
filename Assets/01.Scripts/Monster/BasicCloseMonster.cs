using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// �ٰŸ� Monster�� �⺻ FSM
/// </summary>
public class BasicCloseMonster : StateMachine
{
    private Transform target = null; // Ÿ��
    public float distance => GetDistance(); // Ÿ�ٰ��� �Ÿ�
    public Vector3 dir => GetDirection(); // Ÿ�ٰ��� �Ÿ�

    // ���� ��ũ��Ʈ
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

    // �ʿ� ����
    public float moveRange = 20.0f;
    public float attackRange = 2.5f;
    private float colRadius = 100.0f;
    private float walkingSpeed = 10.0f;

    // �ִϸ��̼� Hash
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

        // ���� �Ҵ�
        idleState = new BasicMonsterIdle(this);
        moveState = new BasicMonsterMove(this);
        attackState = new BasicMonsterAttack(this);
        damageState = new BasicMonsterDamage(this);
        dieState = new BasicMonsterDie(this);

        agent.speed = walkingSpeed;
    }

    // �⺻ State ��������
    protected override BaseState GetInitState() { return idleState; }
    
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
    
    // Ÿ�� ���ϱ�
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

    // Ÿ�� �Ĵٺ���
    public void LookTarget(Transform target)
    {
        Vector3 dir = GetDirection();
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        transform.rotation = rot;
    }

    // ������ �Ծ��� �� ȣ�� (������ ���� ���·� ��ȯ)
    public void Damaged()
    {
        ChangeState(damageState);
    }

    // �浹ó���� ������ �ҰŸ� �̷������� �ϸ� ��
    // �±׷� ���� ���� �Ǵ��ؼ� �� ��ũ��Ʈ�� ������ ȣ���ϴ� ������
    // �����ϸ� �����ϰ� �����ּ���
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="CloseMonster")
        {
            other.GetComponent<BasicCloseMonster>()?.Damaged();
        }
    }
}
