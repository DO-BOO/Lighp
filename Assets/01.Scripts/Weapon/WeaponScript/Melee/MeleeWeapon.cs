using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeapon : WeaponScript
{
    [SerializeField] protected BoxCollider atkArea = null;
    [SerializeField] protected TrailRenderer trail = null;

    #region 무기 범위 증가
    protected void IncreaseCollider(float factor)
    {
        Vector3 size = atkArea.size;
        size.y *= factor;
        Vector3 center = atkArea.center;
        center.y += (size.y - atkArea.size.y) / 2;
        atkArea.size = size;
        atkArea.center = center;
    }

    protected IEnumerator IncreaseCollider(float factor, float time)
    {
        IncreaseCollider(factor);
        yield return new WaitForSeconds(time);
        IncreaseCollider(1 / factor);
    }
    #endregion
}
