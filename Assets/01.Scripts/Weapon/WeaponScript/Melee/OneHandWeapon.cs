using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHandWeapon : WeaponScript
{
    [SerializeField] private Collider atkArea = null;
    [SerializeField] private TrailRenderer trail = null;

    #region �⺻���� ���� �Լ�
    public override void PreDelay()
    {
        
    }
    public override void HitTime()
    {
        atkArea.enabled = true;
        trail.enabled = true;
    }
    public override void PostDelay()
    {
        atkArea.enabled = false;
        trail.enabled = false;
    }

    public override void Stay()
    {

    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        IHittable target = other.GetComponent<IHittable>();
        if(target != null && data.isEnemy != target.isEnemy) //Ÿ���� �Ʊ�/���� Ȯ��
        {
            target.GetDamge(data.damage, data.hitStunTime);
        }
    }
}
