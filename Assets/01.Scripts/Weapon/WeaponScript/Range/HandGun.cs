using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : RangeWeapon
{
    public override void PreDelay()
    {

    }

    public override void HitTime()
    {
        BulletScript obj = GameManager.Instance.Pool.Pop(bulletPrefab.gameObject).GetComponent<BulletScript>();
        obj.transform.SetParent(null);
        Vector3 dir = (GameManager.Instance.GetMousePos() - muzzle.position).normalized;
        Debug.Log(dir);
        dir.y = 0;
        obj.FireBullet(muzzle.position, dir, data);
    }

    public override void PostDelay()
    {

    }

    public override void Stay()
    {

    }

    public override void StopAttack()
    {

    }

    public override void BuffRange(float factor, float time)
    {
        if(time >= 0)
            StartCoroutine(IncreaseBulletScale(factor, time));
        else
            IncreaseBulletScale(factor);
    }
}
