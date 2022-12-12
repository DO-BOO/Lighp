using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHandWeapon : MeleeWeapon
{
    #region 기본공격 관련 함수
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
    public override void StopAttack()
    {
        atkArea.enabled = false;
        trail.enabled = false;
    }
    #endregion

    protected override void Start()
    {
        weaponSkill = new OneHandWeaponSkill_N(parent, data);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == Define.MONSTER_LAYER)
        {
            //StateMachine monster = other.GetComponent<StateMachine>();

            //if (monster)
            {
                marbleController.ExecuteAttack(other.GetComponent<StateMachine>());
                //monster.GetComponent<CharacterHp>()?.Hit((int)Damage);
                if (other.tag == "CLOSE")
                {
                    other.GetComponent<MeleeMonster>()?.Damaged((int)Damage, false);
                }
                else if (other.tag == "FAR")
                {
                    other.GetComponent<FarMonster>()?.Damaged((int)Damage, false);
                }
            }
        }
    }

    public override void BuffRange(float factor, float time)
    {
        if (time >= 0)
            StartCoroutine(IncreaseCollider(factor, time));
        else
            IncreaseCollider(factor);
    }
}
