using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    #region 공격관련 변수
    [SerializeField] private bool isEnemy = false;
    private bool isAttack = false;
    public bool IsAttack => isAttack;
    #endregion

    #region 무기관련 변수
    [SerializeField] private WeaponScript curWeapon = null;
    [SerializeField] private Transform handPosition = null;
    #endregion

    #region 애니메이션관련 변수
    private Animator animator = null;
    private readonly int hashWeaponType = Animator.StringToHash("WeaponType");
    private readonly int hashPreSpeed = Animator.StringToHash("PreSpeed");
    private readonly int hashAttackSpeed = Animator.StringToHash("AttackSpeed");
    private readonly int hashPostSpeed = Animator.StringToHash("PostSpeed");
    private readonly int hashAttack = Animator.StringToHash("Attack");
    //private int hashIsCharging = Animator.StringToHash("IsCharging");
    #endregion

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        SetAnimParam();
        SetEvent();
    }

    private void SetEvent()
    {
        EventManager<InputType>.StartListening((int)InputAction.Attack, OnAttack);
        if (curWeapon != null)
            EventManager<InputType>.StartListening((int)InputAction.WeaponSkill, curWeapon.UseSkill);
        EventManager<InputType>.StartListening((int)InputAction.Dash, OnDash);
    }

    #region 애니메이터 변수 설정 함수
    //목표재생속도 = 현재재생속도 * 현재재생시간(1초) / 목표재생시간
    private void SetAnimParam()
    {
        if(curWeapon != null)
        {
            animator.SetInteger(hashWeaponType, (int)curWeapon.Data.type);
            animator.SetFloat(hashPreSpeed, 1 / curWeapon.Data.preDelay);
            animator.SetFloat(hashAttackSpeed, 1 / curWeapon.Data.HitTime);
            animator.SetFloat(hashPostSpeed, 1 / curWeapon.Data.postDelay);
        }
    }
    #endregion

    //매개변수의 무기를 장착
    private void EquipWeapon(WeaponScript weapon)
    {
        if (curWeapon == null)
        {
            weapon.Reset(handPosition, isEnemy);
            curWeapon = weapon;
            SetAnimParam();
        }
    }

    private void OnAttack(InputType input)
    {
        if (input != InputType.GetKeyDown || curWeapon == null || isAttack) return;
        isAttack = true;
        animator.SetTrigger(hashAttack);
    }

    public void OnDash(InputType type)
    {
        isAttack = false;
        curWeapon.StopAttack();
    }
        
    #region 공격 애니메이션 관련 함수
    //선 딜레이 시작 시
    public void PreDelay()
    {
        curWeapon.PreDelay();
    }

    //선 딜레이 종료 시
    public void HitTime()
    {
        curWeapon.HitTime();
    }

    //후 딜레이 시작 시
    public void PostDelay()
    {
        curWeapon.PostDelay();
        isAttack = false;
    }
    //후 딜레이 종료 시
    public void Stay()
    {
        curWeapon.Stay();
    }
    #endregion
}
