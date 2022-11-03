using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    #region 공격관련 변수
    [SerializeField] private bool isEnemy = false;
    private bool isCanAttack = true;
    #endregion

    #region 무기관련 변수
    [SerializeField] private WeaponScript curWeapon = null;
    [SerializeField] private Transform handPosition = null;
    #endregion

    #region 애니메이션관련 변수
    private Animator animator = null;
    private int hashWeaponType = Animator.StringToHash("WeaponType");
    private int hashPreSpeed = Animator.StringToHash("PreSpeed");
    private int hashAttackSpeed = Animator.StringToHash("AttackSpeed");
    private int hashPostSpeed = Animator.StringToHash("PostSpeed");
    private int hashAttack = Animator.StringToHash("Attack");
    //xprivate int hashIsCharging = Animator.StringToHash("IsCharging");
    #endregion

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        SetAnimParam();
    }

    #region 변수 설정 함수
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

    //나중에 바꿔야할듯
    private void Update()
    {
        if (InputManager.GetKeyDown(InputAction.Attack) && curWeapon != null && isCanAttack)
        {
            Attack();
        }
        if (InputManager.GetKeyDown(InputAction.WeaponSkill) && curWeapon != null)
        {
            curWeapon.UseSkill();
        }
    }

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

    private void Attack()
    {
        isCanAttack = false;
        animator.SetTrigger(hashAttack);
    }

    #region 공격 애니메이션 관련 함수
    //선 딜레이 시작 시
    public void PreDelay()
    {
    //    if(curWeapon.Data.chargingTime > 0)
    //    {
    //        StartCoroutine(ChargingCoroutine());
    //    }
        curWeapon.PreDelay();
    }

    //private IEnumerator ChargingCoroutine()
    //{
    //    animator.SetBool(hashIsCharging, true);
    //    float t = curWeapon.Data.preDelay / 2;
    //    while (t > 0)
    //    {
    //        if (InputManager.GetkeyUp(InputAction.Attack))
    //        {
    //            animator.SetBool(hashIsCharging, false);
    //            yield break;
    //        }
    //        t -= Time.deltaTime;
    //        yield return null;
    //    }
    //    t = 0;
    //    while (true)
    //    {
    //        if (InputManager.GetkeyUp(InputAction.Attack))
    //        {
    //            animator.SetBool(hashIsCharging, false);
    //            curWeapon.Data.chargingAmount = t;
    //            yield break;
    //        }
    //        t += Time.deltaTime;
    //        yield return null;
    //    }
    //}

    //선 딜레이 종료 시
    public void HitTime()
    {
        curWeapon.HitTime();
    }

    //후 딜레이 시작 시
    public void PostDelay()
    {
        curWeapon.PostDelay();

    }
    //후 딜레이 종료 시
    public void Stay()
    {
        isCanAttack = true;
        curWeapon.Stay();
    }
    #endregion
}
