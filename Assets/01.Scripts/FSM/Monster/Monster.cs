using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Monster : StateMachine
{
    protected Character character; // Component
    protected CharacterHp monsterHP; // HP

    // Layer
    public LayerMask targetLayerMask;
    public LayerMask blockLayerMask;

    public float moveRange = 15.0f;
    public float attackRange = 12.0f;

    private void Awake()
    {
        SetStates();
    }

    private void ResetMonster()
    {
        monsterHP.Hp = monsterHP.MaxHp;
    }

    #region GET

    public float distance => GetDistance(); // Ÿ�ٰ��� �Ÿ�
    public Vector3 dir => GetDirection(); // Ÿ�ٰ��� �Ÿ�

    // Ÿ�ٰ��� �Ÿ� ���ϱ�
    protected override float GetDistance() { return Vector3.Distance(target.transform.position, transform.position); }

    // Ÿ�ٰ��� ���� ���ϱ�
    protected override Vector3 GetDirection()
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;
        return dir;
    }

    #endregion

    #region TARGET

    private Transform target = null; // Ÿ��
    private const float colRadius = 100f;

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

    #region STATE

    protected override BaseState GetInitState() { return states[typeof(BasicMonsterIdle)]; } // �⺻ State ��������

    // ���� ��ũ��Ʈ
    public Dictionary<Type, BaseState> states;
    private void SetStates()
    {
        states.Clear();
        states[typeof(BasicMonsterIdle)] = new BasicMonsterIdle(this);
        states[typeof(BasicMonsterMove)] = new BasicMonsterMove(this);
        states[typeof(BasicMonsterStun)] = new BasicMonsterStun(this);
        states[typeof(BasicMonsterDamage)] = new BasicMonsterDamage(this);
        states[typeof(BasicMonsterDie)] = new BasicMonsterDie(this);
        StateInit();
    }
    protected virtual void StateInit() { }

    #endregion

    #region ANIMATION

    public void AnimationPlay(int hash, bool isOn)
    {
        character.anim.SetBool(hash, isOn);
    }
    
    public void AnimationPlay(int hash)
    {
        character.anim.SetTrigger(hash);
    }

    #endregion

    #region DAMAGE

    // ������ �Ծ��� �� ȣ�� (������ ���� ���·� ��ȯ)
    public void Damaged(bool isStun)
    {
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

    #endregion

}
