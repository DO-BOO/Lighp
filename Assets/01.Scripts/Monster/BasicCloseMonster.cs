using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Monster의 기본 FSM
/// </summary>
public class BasicCloseMonster : StateMachine
{
    private Transform target = null;
    public float distance => GetDistance();

    public BasicMonsterIdle idleState;
    public BasicMonsterMove moveState;
    public BasicMonsterAttack attackState;
    public BasicMonsterDamage damageState;
    public BasicMonsterDie dieState;

    public LayerMask targetLayerMask;
    public LayerMask blockLayerMask;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Animator anim;

    public float moveRange = 20.0f;
    public float attackRange = 10.0f;
    private float colRadius = 50.0f;
    private float walkingSpeed = 10.0f;

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

        idleState = new BasicMonsterIdle(this);
        moveState = new BasicMonsterMove(this);
        attackState = new BasicMonsterAttack(this);
        damageState = new BasicMonsterDamage(this);
        dieState = new BasicMonsterDie(this);

        agent.speed = walkingSpeed;
    }

    // 기본 State 가져오기
    protected override BaseState GetInitState() { return idleState; }
    
    // 타겟과의 거리 구하기
    protected override float GetDistance()
    {
        return Vector3.Distance(target.transform.position, transform.position);
    }
    
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

    public void MoveAnimation(bool isOn)
    {
        //anim.SetBool(hashWalk, isOn);
    }

    public void AttackAnimation(bool isOn)
    {
        //anim.SetBool(hashAttack, isOn);
    }

    public void DieAnimation(bool isOn)
    {
        //anim.SetBool(hashDie, isOn);
    }
    public void DamageAnimation()
    {
        //anim.SetTrigger(hashDamage);
    }
}
