using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicFarMonster : StateMachine
{
    private Transform target = null;
    public float distance => GetDistance();

    public FarMonsterIdle idleState;
    public FarMonsterMove moveState;
    public FarMonsterAttack attackState;
    public FarMonsterDamage damageState;
    public FarMonsterDie dieState;

    public LayerMask targetLayerMask;
    public LayerMask blockLayerMask;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Animator anim;

    public float moveRange = 25.0f; // ���� �Ÿ� �̻��� �Ǹ� �Ѿư�
    public float attackRange = 25.0f; // ���� �Ÿ� �ȿ� ������ ����
    private float colRadius = 50.0f; // �ν� �ݶ��̴� ������
    private float walkingSpeed = 10.0f; // �Ѿư��� ���ǵ�

    public GameObject bullet;

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

        idleState = new FarMonsterIdle(this);
        moveState = new FarMonsterMove(this);
        attackState = new FarMonsterAttack(this);
        damageState = new FarMonsterDamage(this);
        dieState = new FarMonsterDie(this);

        agent.speed = walkingSpeed;
    }

    // �⺻ State ��������
    protected override BaseState GetInitState() { return idleState; }

    // Ÿ�ٰ��� �Ÿ� ���ϱ�
    protected override float GetDistance()
    {
        return Vector3.Distance(target.transform.position, transform.position);
    }

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
}
