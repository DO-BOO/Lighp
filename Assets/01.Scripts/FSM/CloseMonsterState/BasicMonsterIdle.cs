using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// (�⺻) ���� IDLE ���� 
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

    public override void Enter()
    {
        base.Enter();
        target = monster.SerachTarget();
    }

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

    public override void UpdateLate()
    {
        base.UpdateLogic();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
