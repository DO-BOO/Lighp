using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    #region ���ݰ��� ����
    [SerializeField] private bool isEnemy = false;
    private bool isAttack;
    public bool IsAttack => isAttack;
    private bool isDraw;
    public bool IsCanAttack => !isAttack && !isDraw;
    #endregion

    #region ������� ����
    [SerializeField] private Transform handPosition = null;
    [SerializeField] private List<WeaponScript> startingWeapons = new List<WeaponScript>();
    [SerializeField] private WeaponScript[] weapons;
    [SerializeField] private int maxWeaponCnt = 2;
    private int curWeaponCnt = 0;
    private int curWeaponIndex = 0;
    private WeaponScript curWeapon => weapons[curWeaponIndex];
    #endregion

    #region �ִϸ��̼ǰ��� ����
    private Animator animator = null;
    private readonly int hashWeaponType = Animator.StringToHash("WeaponType");
    private readonly int hashPreSpeed = Animator.StringToHash("PreSpeed");
    private readonly int hashAttackSpeed = Animator.StringToHash("AttackSpeed");
    private readonly int hashPostSpeed = Animator.StringToHash("PostSpeed");
    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashDraw = Animator.StringToHash("Draw");
    //private int hashIsCharging = Animator.StringToHash("IsCharging");
    #endregion

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        weapons = new WeaponScript[maxWeaponCnt];
        SetEvent();
    }

    private void SetEvent()
    {
        EventManager<InputType>.StartListening((int)InputAction.Attack, OnAttack);
        EventManager<InputType>.StartListening((int)InputAction.Dash, OnDash);
        EventManager<InputType, InputAction>.StartListening((int)InputAction.FirstWeapon, SelectWeapon);
        EventManager<InputType, InputAction>.StartListening((int)InputAction.SecondWeapon, SelectWeapon);
        EventManager.StartListening(Define.ON_END_READ_DATA, SetWeapons);
    }

    //���� �κ��丮 �ʱ� ����
    private void SetWeapons()
    {
        if (startingWeapons.Count > 0)
        {
            foreach (WeaponScript weapon in startingWeapons)
            {
                WeaponScript newWeapon = Instantiate(weapon, transform);
                GetWeapon(newWeapon);
            }

            SelectWeapon(0);
        }
    }

    #region �ִϸ����� ���� ���� �Լ�
    //��ǥ����ӵ� = ��������ӵ� * ��������ð�(1��) / ��ǥ����ð�
    private void SetAnimParam()
    {
        if (curWeapon != null)
        {
            animator.SetInteger(hashWeaponType, (int)curWeapon.Data.type);
            animator.SetFloat(hashPreSpeed, 1 / curWeapon.Data.preDelay);
            animator.SetFloat(hashAttackSpeed, 1 / curWeapon.Data.HitTime);
            animator.SetFloat(hashPostSpeed, 1 / curWeapon.Data.postDelay);
        }
    }
    #endregion


    public void GetWeapon(WeaponScript weapon)
    {
        if (curWeaponCnt >= maxWeaponCnt)
        {
            weapon.Equip(handPosition, isEnemy);
            weapons[curWeaponIndex] = weapon;
            SelectWeapon(curWeaponIndex);
        }
        else
        {
            weapon.Equip(handPosition, isEnemy);
            curWeaponCnt++;
            weapons[curWeaponCnt - 1] = weapon;
            SelectWeapon(curWeaponCnt - 1);
        }
    }

    //�ӽ÷� ���� �Է� �Լ� ���� ���� ����
    public void SelectWeapon(InputType type, InputAction action)
    {
        if (type != InputType.GetKeyDown) return;
        if (action == InputAction.FirstWeapon)
        {
            SelectWeapon(0);
        }
        else if (action == InputAction.SecondWeapon)
        {
            SelectWeapon(1);
        }
    }

    public void SelectWeapon(int index)
    {
        if (weapons[index] == null) return;

        int previousIdx = curWeaponIndex;
        curWeapon.gameObject.SetActive(false);
        curWeaponIndex = index;
        curWeapon.gameObject.SetActive(true);
        SetAnimParam();
        animator.SetTrigger(hashDraw);

        EventManager<WeaponScript, WeaponScript>.TriggerEvent(Define.ON_SET_WEAPON, weapons[previousIdx], weapons[curWeaponIndex]);
    }

    //���� ���� �� �ִϸ��̼� ����
    private void OnAttack(InputType input)
    {
        if (input != InputType.GetKeyDown || curWeapon == null || !IsCanAttack) return;
        isAttack = true;
        animator.SetTrigger(hashAttack);
    }

    //�뽬 ��� �� ��� ĵ��
    public void OnDash(InputType type)
    {
        isAttack = false;
        isDraw = false;
        curWeapon.StopAttack();
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
        isAttack = false;
    }
    //�� ������ ���� ��
    public void Stay()
    {
        curWeapon.Stay();
    }

    //���� ���� �ִϸ��̼� �����
    public void OnDrawEnd()
    {
        isDraw = false;
    }
    #endregion

    private void OnDestroy()
    {
        EventManager<InputType>.StopListening((int)InputAction.Attack, OnAttack);
        if (curWeapon != null)
            EventManager<InputType>.StopListening((int)InputAction.WeaponSkill, curWeapon.UseSkill);
        EventManager<InputType>.StopListening((int)InputAction.Dash, OnDash);
        EventManager.StopListening(Define.ON_END_READ_DATA, SetWeapons);
    }
}
