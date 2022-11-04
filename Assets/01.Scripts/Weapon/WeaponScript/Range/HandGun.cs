using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : WeaponScript
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private BulletScript bulletPrefab;

    public override void PreDelay()
    {

    }
    //���߿� Ǯ������ ���ľ���
    public override void HitTime()
    {
        BulletScript obj = Instantiate(bulletPrefab, transform);
        obj.transform.SetParent(null);
        Vector3 dir = (GameManager.Instance.GetMousePos() - muzzle.position).normalized;
        dir.y = 0;
        obj.FireBullet(muzzle.position, dir, data.damage, data.hitStunTime, data.isEnemy);
    }

    public override void PostDelay()
    {

    }

    public override void Stay()
    {

    }
}
