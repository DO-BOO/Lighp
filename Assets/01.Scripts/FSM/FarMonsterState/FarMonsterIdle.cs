using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// (�⺻) ���Ÿ� ���� IDLE ���� 
/// </summary>
public class FarMonsterIdle : BaseState
{
    BasicFarMonster monster;
    Transform target = null;

    // IDLE ���� ����
    public FarMonsterIdle(BasicFarMonster stateMachine) : base("IDLE", stateMachine)
    {
        monster = (BasicFarMonster)stateMachine;
    }

    // ���� ���� ��
    // Ÿ�� ã�� ����
    public override void Enter()
    {
        base.Enter();
        target = monster.SerachTarget();
    }

    // Ÿ���� �ִٸ�
    // �Ÿ��� ���� ���� ��ȯ
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        target = monster.SerachTarget();

        if (target)
        {
            if (monster.distance <= monster.attackRange)
            {
                stateMachine.ChangeState(((BasicFarMonster)stateMachine).attackState);
            }
            else if (monster.distance >= monster.moveRange)
            {
                stateMachine.ChangeState(((BasicFarMonster)stateMachine).moveState);
            }
        }
    }

    // ���� ������ ���� LateUpdate
    public override void UpdateLate()
    {
        base.UpdateLogic();
    }

    // ���� ������ ��
    public override void Exit()
    {
        base.Exit();
    }
}
