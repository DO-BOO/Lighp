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
   

    // IDLE ���� ����
    public BasicMonsterIdle(BasicMonster stateMachine) : base("IDLE", stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            stateMachine.ChangeState(((BasicMonster)stateMachine).moveState);
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
