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
        EventManager<InputType>.StartListening((int)InputAction.Attack, Attack);
        if (curWeapon != null)
            EventManager<InputType>.StartListening((int)InputAction.WeaponSkill, curWeapon.UseSkill);
    }

    #region �ִϸ����� ���� ���� �Լ�
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

    private void Attack(InputType input)
    {
        if (input != InputType.GetKeyDown || curWeapon == null || !isCanAttack) return;
        isCanAttack = false;
        animator.SetTrigger(hashAttack);
    }
        
    #region ���� �ִϸ��̼� ���� �Լ�
    //�� ������ ���� ��
    public void PreDelay()
    {
        curWeapon.PreDelay();
    }

    //�� ������ ���� ��
    public void HitTime()
    {
        curWeapon.HitTime();
    }

    //�� ������ ���� ��
    public void PostDelay()
    {
        curWeapon.PostDelay();
        isCanAttack = true;
    }
    //�� ������ ���� ��
    public void Stay()
    {
        curWeapon.Stay();
    }
    #endregion
}
