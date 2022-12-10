using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHandWeapon : MeleeWeapon
{
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
    public override void StopAttack()
    {
        atkArea.enabled = false;
        trail.enabled = false;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if(1 << other.gameObject.layer == Define.MONSTER_LAYER)
        {
            StateMachine monster = other.GetComponent<StateMachine>();

            if (monster)
            {
                marbleController.ExecuteAttack(other.GetComponent<StateMachine>());
<<<<<<< HEAD
                monster.GetComponent<BasicCloseMonster>()?.Damaged(false);
=======
                //monster.GetComponent<CharacterHp>()?.Hit((int)Damage);
                monster.GetComponent<MeleeMonster>()?.Damaged(false);
>>>>>>> Re_FSM
            }
        }
    }

    public override void BuffRange(float factor, float time)
    {
        if(time >= 0)
            StartCoroutine(IncreaseCollider(factor, time));
        else
            IncreaseCollider(factor);
    }
}
