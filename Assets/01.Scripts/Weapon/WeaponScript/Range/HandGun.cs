using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : WeaponScript
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private BulletScript bulletPrefab;

    protected override void Start()
    {
        base.Start();
        GameManager.Instance.Pool.CreatePool(bulletPrefab.gameObject, 10);
    }

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
        obj.FireBullet(muzzle.position, dir, data.damage, data.hitStunTime, data.isEnemy);
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
}
