//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.AI;
//using static UnityEditor.PlayerSettings;

//public class BasicFarMonster : StateMachine
//{
//    private Transform target = null; // Ÿ��

//    // ���� ��ũ��Ʈ
//    public FarMonsterIdle idleState;
//    public FarMonsterMove moveState;
//    public FarMonsterStun stunState;
//    public FarMonsterAttack attackState;
//    public FarMonsterDamage damageState;
//    public FarMonsterDie dieState;

//    // Layer
//    public LayerMask targetLayerMask;
//    public LayerMask blockLayerMask;

//    // Component
//    [HideInInspector]
//    public NavMeshAgent agent;
//    [HideInInspector]
//    public Animator anim;
//    [HideInInspector]
//    public Rigidbody rigid;
//    [HideInInspector]
//    public CapsuleCollider collider;

//    // �ʿ� ����
//    public float moveRange = 25.0f; // ���� �Ÿ� �̻��� �Ǹ� �Ѿư�
//    public float attackRange = 25.0f; // ���� �Ÿ� �ȿ� ������ ����
//    private float colRadius = 100.0f; // �ν� �ݶ��̴� ������
//    private float walkingSpeed = 10.0f; // �Ѿư��� ���ǵ�


//    public float GetHP => HP;
//    private const float MAX_HP = 100;
//    private float HP = 100f;
//    private bool live = true;
//    public bool LIVE => live;

//    // �߻�ü
//    public GameObject bullet;

//    private void Awake()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        anim = GetComponent<Animator>();
//        rigid = GetComponent<Rigidbody>();
//        collider = GetComponent<CapsuleCollider>();

//        // ���� �Ҵ�
//        dieState = new FarMonsterDie(this);
//        idleState = new FarMonsterIdle(this);
//        stunState = new FarMonsterStun(this);
//        moveState = new FarMonsterMove(this);
//        attackState = new FarMonsterAttack(this);
//        damageState = new FarMonsterDamage(this);

//        SetMonsterInform();
//    }

//    #region SET

//    public void SetMonsterInform()
//    {
//        live = true;
//        agent.speed = walkingSpeed;
//        agent.stoppingDistance = attackRange;
//    }

//    #endregion

//    #region GET
//    public float distance => GetDistance(); // Ÿ�ٰ��� �Ÿ�
//    public Vector3 dir => GetDirection(); // Ÿ�ٰ��� �Ÿ�
//    // �⺻ State ��������
//    protected override BaseState GetInitState() { return idleState; }

//    // Ÿ�ٰ��� �Ÿ� ���ϱ�
//    protected override float GetDistance()
//    {
//        return Vector3.Distance(target.transform.position, transform.position);
//    }

//    // Ÿ�ٰ��� ���� ���ϱ�
//    protected override Vector3 GetDirection()
//    {
//        Vector3 dir = target.position - transform.position;
//        dir.y = 0;
//        return dir;
//    }

//    #endregion

//    #region TARGET

//    // Ÿ�� ã��
//    public Transform SerachTarget()
//    {
//        Collider[] cols = Physics.OverlapSphere(transform.position, colRadius, targetLayerMask);
//        if (cols.Length > 0)
//        {
//            target = cols[0].gameObject.transform;
//            return target;
//        }
//        else return null;
//    }

//    // Ÿ�� �Ĵٺ��� �Լ�
//    public void LookTarget(Transform target)
//    {
//        Vector3 dir = target.position - transform.position;
//        dir.y = 0;
//        Quaternion rot = Quaternion.LookRotation(dir.normalized);
//        transform.rotation = rot;
//    }

//    #endregion

//    #region Damage

//    // ������ �Ծ��� �� ȣ��
//    // ������ ���� ���·� ��ȯ
//    public void Damaged(bool isStun)
//    {
//        if (!live) return;
//        if (isStun)        ChangeState(stunState);
//        else         ChangeState(damageState);
//    }

//    public void SetHP(bool isHeal, float plusHP)
//    {
//        if (isHeal)
//        {
//            HP += plusHP;
//        }
//        else
//        {
//            HP -= plusHP;
//        }
//        if (HP <= 0)
//        {
//            live = false;
//            ChangeState(dieState);
//        }
//    }

//    public void ReviveHP()
//    {
//        HP = MAX_HP;
//    }


//    #endregion

//    #region Shoot

//    // �߻�ü �߻� �Լ�
//    // ���� ���ϰ� �߻�ü ���� ����
//    // PoolManager ���
//    public void Shooting()
//    {
//        Vector3 pos = new Vector3(transform.position.x+2.0f, 1.0f, transform.position.z);
//        Transform tDir = SerachTarget();
//        Vector3 direction = (tDir.position - pos).normalized;
//        Quaternion lookRotation = Quaternion.LookRotation(direction);
//        FarMonsterBullet obj = GameManager.Instance.Pool.Pop("BasicFarMonsterBullet", null, pos, lookRotation) as FarMonsterBullet;
//        obj.gameObject.SetActive(true);
//    }

//    #endregion

//    #region ANIMATION

//    // �ִϸ��̼� Hash
//    [HideInInspector]
//    public int hashWalk = Animator.StringToHash("Walk");
//    [HideInInspector]
//    public int hashAttack = Animator.StringToHash("Attack");
//    [HideInInspector]
//    public int hashDamage = Animator.StringToHash("Damage");
//    [HideInInspector]
//    public int hashDie = Animator.StringToHash("Die");
//    [HideInInspector]
//    public int hashStun = Animator.StringToHash("Stun");

//    // �̵� �ִϸ��̼�
//    public void MoveAnimation(bool isOn)
//    {
//        anim.SetBool(hashWalk, isOn);
//    }

//    // ���� �ִϸ��̼�
//    public void AttackAnimation(bool isOn)
//    {
//        anim.SetBool(hashAttack, isOn);
//    }

//    // ���� �ִϸ��̼�
//    public void DieAnimation(bool isOn)
//    {
//        anim.SetBool(hashDie, isOn);
//    }

//    // ������ �Դ� �ִϸ��̼�
//    public void DamageAnimation()
//    {
//        anim.SetTrigger(hashDamage);
//    }
    
//    // ���� �ִϸ��̼�
//    public void StunAnimation(bool isOn)
//    {
//        anim.SetBool(hashStun, isOn);
//    }

//    #endregion
//}
