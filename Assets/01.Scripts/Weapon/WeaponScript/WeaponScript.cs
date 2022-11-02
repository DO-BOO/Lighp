using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponScript : MonoBehaviour
{
    protected int weaponNumber = 0;

    [SerializeField] protected WeaponData data = null;
    public WeaponData Data => data;

    [SerializeField] private WeaponSkill skill = null;


    //�� ������ ����
    public abstract void PreDelay();

    //�� ������ ����
    public abstract void HitTime();

    //�� ������ ����
    public abstract void PostDelay();

    //�� ������ ����
    public abstract void Stay();

    public void UseSkill()
    {
        skill.UseSkill();
    }
}
