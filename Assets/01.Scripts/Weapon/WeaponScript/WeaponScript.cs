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

    protected float Damage => marbleController.PowerWeight * data.damage;
    //protected float Range => marbleController.PowerWeight * data.range;
    protected float CoolTime => marbleController.SpeedWeight * data.atkCool;
    #endregion

    protected virtual void Start()
    {
        marbleController = new MarbleController(gameObject);
    }

    //선 딜레이 시작
    public abstract void PreDelay();

    //선 딜레이 종료
    public abstract void HitTime();

    //후 딜레이 시작
    public abstract void PostDelay();

    //후 딜레이 종료
    public abstract void Stay();

    //공격 강제 종료
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