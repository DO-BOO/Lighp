using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHandWeaponSkill : WeaponSkill
{
    public OneHandWeaponSkill(WeaponParent parent, WeaponData data) : base(parent, data){}

    public override void Hit()
    {
        BulletScript bullet = GameManager.Instance.Pool.Pop(GetType().Name) as BulletScript;
        Vector3 pos = parent.transform.position;
        pos += Vector3.up * 3.2f;
        pos += parent.transform.forward * 2f;

        bullet.FireBullet(pos, parent.transform.forward, data, false);
    }
}
