using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class BasicFarMonster : StateMachine
{
    private Transform target = null; // Ÿ��
    public float distance => GetDistance(); // Ÿ�ٰ��� �Ÿ�

    // ���� ��ũ��Ʈ
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

    // �ʿ� ����
    public float moveRange = 25.0f; // ���� �Ÿ� �̻��� �Ǹ� �Ѿư�
    public float attackRange = 25.0f; // ���� �Ÿ� �ȿ� ������ ����
    private float colRadius = 100.0f; // �ν� �ݶ��̴� ������
    private float walkingSpeed = 10.0f; // �Ѿư��� ���ǵ�

    // �ִϸ��̼� Hash
    [HideInInspector]
    public int hashWalk = Animator.StringToHash("Walk");
    [HideInInspector]
    public int hashAttack = Animator.StringToHash("Attack");
    [HideInInspector]
    public int hashDamage = Animator.StringToHash("Damage");
    [HideInInspector]
    public int hashDie = Animator.StringToHash("Die");

    // �߻�ü
    public GameObject bullet;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // ���� �Ҵ�
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

    // Ÿ�� ã��
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

    // Ÿ�� �Ĵٺ��� �Լ�
    public void LookTarget(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        transform.rotation = rot;
    }

    // ������ �Ծ��� �� ȣ��
    // ������ ���� ���·� ��ȯ
    public void Damaged()
    {
        ChangeState(damageState);
    }

    // �߻�ü �߻� �Լ�
    // ���� ���ϰ� �߻�ü ���� ����
    // PoolManager ���
    public void Shooting()
    {
        Vector3 pos = new Vector3(transform.position.x+2.0f, 1.0f, transform.position.z);
        Transform tDir = SerachTarget();
        Vector3 direction = (tDir.position - pos).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        FarMonsterBullet obj = GameManager.Instance.Pool.Pop("BasicFarMonsterBullet", null, pos, lookRotation) as FarMonsterBullet;
        obj.gameObject.SetActive(true);
    }

}
