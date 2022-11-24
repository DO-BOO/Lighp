using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponScript : MonoBehaviour
{
    protected int weaponNumber = 0;
    [SerializeField] protected WeaponData data = null;
    public WeaponData Data => data;
    [SerializeField] private WeaponSkill skill = null;
    protected WeaponParent parent = null;

    #region Element Marble
    [SerializeField]
    protected MarbleController marbleController;
    public MarbleController MarbleController => marbleController;

    protected float Damage => marbleController.PowerWeight + data.damage;
    protected float Range => (1f + marbleController.PowerWeight * 0.01f) * data.range;
    protected float CoolTime => (1f - marbleController.SpeedWeight * 0.01f) * data.atkCool;
    #endregion

    protected virtual void Start()
    {
        marbleController = new MarbleController(gameObject);
    }

    //�� ������ ����
    public abstract void PreDelay();

    //�� ������ ����
    public abstract void HitTime();

    //�� ������ ����
    public abstract void PostDelay();

    //�� ������ ����
    public abstract void Stay();

    //���� ���� ����
    public abstract void StopAttack();

    public void UseSkill(InputType type)
    {

    }

    public void Reset(Transform handle, bool isEnemy)
    {
        transform.SetParent(handle);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localPosition = Vector3.zero;
        data.isEnemy = isEnemy;
    }
}