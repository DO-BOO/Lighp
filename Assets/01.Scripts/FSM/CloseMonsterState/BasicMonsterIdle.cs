using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// (�⺻) �ٰŸ� ���� IDLE ���� 
/// </summary>
public class BasicMonsterIdle : BaseState
{
    BasicCloseMonster monster;
    Transform target = null;

    // IDLE ���� ����
    public BasicMonsterIdle(BasicCloseMonster stateMachine) : base("IDLE", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
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

        //if (Input.GetKeyDown(KeyCode.Escape))
        target = monster.SerachTarget();

        if (target)
        {
            if (monster.distance <= monster.attackRange)
            {
                stateMachine.ChangeState(((BasicCloseMonster)stateMachine).attackState);
            }
            else if (monster.distance <= monster.moveRange)
            {
                stateMachine.ChangeState(((BasicCloseMonster)stateMachine).moveState);
            }
        }
    }

    // ���� ������ ��
    public override void Exit()
    {
        base.Exit();
    }
}
