using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    #region ���ݰ��� ����
    [SerializeField] private bool isEnemy = false;
    private bool isCanAttack = true;
    #endregion

    #region ������� ����
    [SerializeField] private WeaponScript curWeapon = null;
    [SerializeField] private Transform handPosition = null;
    #endregion

    #region �ִϸ��̼ǰ��� ����
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

    #region ���� ���� �Լ�
    //��ǥ����ӵ� = ��������ӵ� * ��������ð�(1��) / ��ǥ����ð�
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

    //���߿� �ٲ���ҵ�
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

    //�Ű������� ���⸦ ����
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

    #region ���� �ִϸ��̼� ���� �Լ�
    //�� ������ ���� ��
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

    //�� ������ ���� ��
    public void HitTime()
    {
        curWeapon.HitTime();
    }

    //�� ������ ���� ��
    public void PostDelay()
    {
        curWeapon.PostDelay();

    }
    //�� ������ ���� ��
    public void Stay()
    {
        isCanAttack = true;
        curWeapon.Stay();
    }
    #endregion
}
