using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���Ÿ� ���� ������ ��ũ��Ʈ
/// </summary>
public class FarMonsterDamage : BaseState
{
    BasicFarMonster monster;

    float delayTime = 1.0f; // ���� �ð�
    float nowDelay = 0.0f;

    float HP = 100f; // ü��
    float damage = 20f; // ������ ���� ũ��

    // ������
    public FarMonsterDamage(BasicFarMonster stateMachine) : base("DAMAGED", stateMachine)
    {
        monster = (BasicFarMonster)stateMachine;
    }

    // ���� ���� ��
    // ���ط� ��ŭ ������ ���� + �ִϸ��̼� ����
    // ���� HP�� 0 ���ϰ� �Ǹ� ���� Die�� ��ȯ
    public override void Enter()
    {
        base.Enter();
        nowDelay = 0;
        HP -= damage;
        if (HP <= 0)
        {
            stateMachine.ChangeState(((BasicFarMonster)stateMachine).dieState);
        }
        monster.anim.SetTrigger(monster.hashDamage);
    }

    // ���� �ð��� ������ ���� ��ȯ
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        nowDelay += Time.deltaTime;
        if (nowDelay >= delayTime)
        {
            stateMachine.ChangeState(((BasicFarMonster)stateMachine).idleState);
        }

    }

    // ���� ������ ��
    public override void Exit()
    {
        base.Exit();
    }
}
