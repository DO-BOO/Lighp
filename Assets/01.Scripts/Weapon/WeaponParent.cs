using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    #region ���ݰ��� ����
    [SerializeField] private bool isEnemy = false;
    private bool isAttack = false;
    public bool IsAttack => isAttack;
    #endregion

    #region ������� ����
    [SerializeField] private List<WeaponScript> startingWeapons = new List<WeaponScript>();
    [SerializeField] private List<WeaponScript> weapons = new List<WeaponScript>();
    [SerializeField] private Transform handPosition = null;
    private int weaponIndex;
    private WeaponScript curWeapon
    {
        get
        {
            return weapons[weaponIndex];
        }
    }
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
        SetEvent();
    }

    private void Start()
    {
        if(startingWeapons.Count > 0)
        {
            foreach(WeaponScript weapon in startingWeapons)
            {
                WeaponScript newWeapon = Instantiate(weapon, transform);
                GetWeapon(newWeapon);
            }
            SelectWeapon(0);
        }
    }

    private void SetEvent()
    {
        EventManager<InputType>.StartListening((int)InputAction.Attack, OnAttack);
        EventManager<InputType>.StartListening((int)InputAction.Dash, OnDash);
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
    private void GetWeapon(WeaponScript weapon)
    {
        weapons.Add(weapon);
        weapon.Equip(handPosition, isEnemy);
        SelectWeapon(weapons.Count - 1);
    }

    private void SelectWeapon(int index)
    {
        curWeapon.gameObject.SetActive(false);
        weaponIndex = index;
        curWeapon.gameObject.SetActive(true);
        SetAnimParam();
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
    #endregion
}
