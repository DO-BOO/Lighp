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

    public float distance => GetDistance(); // 타겟과의 거리
    public Vector3 dir => GetDirection(); // 타겟과의 거리

    // 타겟과의 거리 구하기
    protected override float GetDistance() { return Vector3.Distance(target.transform.position, transform.position); }

    // 타겟과의 방향 구하기
    protected override Vector3 GetDirection()
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;
        return dir;
    }

    #endregion

    #region TARGET

    private Transform target = null; // 타겟
    private const float colRadius = 100f;

    // 타겟 구하기
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

    // 타겟 쳐다보기
    public void LookTarget(Transform target)
    {
        Vector3 dir = GetDirection();
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        transform.rotation = rot;
    }


    #endregion

    #region STATE

    protected override BaseState GetInitState() { return states[typeof(BasicMonsterIdle)]; } // 기본 State 가져오기

    // 상태 스크립트
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

    // 데미지 입었을 때 호출 (데미지 입은 상태로 전환)
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
