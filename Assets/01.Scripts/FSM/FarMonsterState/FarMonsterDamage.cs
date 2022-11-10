using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���Ÿ� ���� ������ ��ũ��Ʈ
/// </summary>
public class FarMonsterDamage : BaseState
{
    BasicFarMonster monster;

    // ������
    public FarMonsterDamage(BasicFarMonster stateMachine) : base("DAMAGED", stateMachine)
    {
        monster = (BasicFarMonster)stateMachine;
    }

    #region DAMAGE
    float delayTime = 1.0f; // ���� �ð�
    float nowDelay = 0.0f;

    float HP = 100f; // ü��
    float damage = 20f; // ������ ���� ũ��

    public float GetHP => HP;

    private void SetHP(bool isHeal, float plusHP)
    {
        if (isHeal)
        {
            HP += plusHP;
        }
        else
        {
            HP -= plusHP;
        }
    }

    private void SetDelay(float delay)
    {
        nowDelay = delay;
    }

    #endregion

    #region ANIMATION

    public override void SetAnim()
    {
        base.SetAnim();
        monster.DamageAnimation();
    }

    #endregion

    #region STATE

    public override void CheckDistance()
    {
        base.CheckDistance();
        if (HP <= 0)
        {
            stateMachine.ChangeState(monster.dieState);
        }
        if (nowDelay >= delayTime)
        {
            stateMachine.ChangeState(monster.idleState);
        }

    }

    // ���� ���� ��
    // ���ط� ��ŭ ������ ���� + �ִϸ��̼� ����
    // ���� HP�� 0 ���ϰ� �Ǹ� ���� Die�� ��ȯ
    public override void Enter()
    {
        base.Enter();
        SetDelay(0);
        SetHP(false, damage);
        SetAnim();
    }

    // ���� �ð��� ������ ���� ��ȯ
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        nowDelay += Time.deltaTime;
    }

    // ���� ������ ��
    public override void Exit()
    {
        base.Exit();
    }

    #endregion
}
