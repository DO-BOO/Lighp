using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarMonsterIdle : BaseState
{
    BasicFarMonster monster;
    Transform target = null;

    // IDLE 상태 정의
    public FarMonsterIdle(BasicFarMonster stateMachine) : base("IDLE", stateMachine)
    {
        monster = (BasicFarMonster)stateMachine;
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
                stateMachine.ChangeState(((BasicFarMonster)stateMachine).attackState);
            }
            else if (monster.distance >= monster.moveRange)
            {
                stateMachine.ChangeState(((BasicFarMonster)stateMachine).moveState);
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
