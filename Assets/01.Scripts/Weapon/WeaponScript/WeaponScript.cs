using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponScript : MonoBehaviour
{
    protected int weaponNumber = 0;

    [SerializeField] protected WeaponData data = null;
    public WeaponData Data => data;

    [SerializeField] private WeaponSkill skill = null;


    //선 딜레이 시작
    public abstract void PreDelay();

    //선 딜레이 종료
    public abstract void HitTime();

    //후 딜레이 시작
    public abstract void PostDelay();

    //후 딜레이 종료
    public abstract void Stay();

    public void UseSkill()
    {
        skill.UseSkill();
    }
}
