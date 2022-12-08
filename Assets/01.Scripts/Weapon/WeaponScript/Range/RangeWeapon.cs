using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangeWeapon : WeaponScript
{
    [SerializeField] protected Transform muzzle;
    [SerializeField] protected BulletScript bulletPrefab;
    protected float bulletScaleFactor = 1;

    protected override void Start()
    {
        base.Start();
        GameManager.Instance.Pool.CreatePool(bulletPrefab.gameObject, (int)(1 / data.atkCool * 3));
    }

    protected void IncreaseBulletScale(float factor)
    {
        bulletScaleFactor *= factor;
    }

    protected IEnumerator IncreaseBulletScale(float factor, float time)
    {
        IncreaseBulletScale(factor);
        yield return new WaitForSeconds(time);
        IncreaseBulletScale(1 / factor);
    }
}
