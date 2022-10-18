using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMonsterMove : BaseState
{
    public BasicMonsterMove(BasicMonster stateMachine) : base("MOVE", stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if(Input.GetKeyDown(KeyCode.Escape))
        stateMachine.ChangeState(((BasicMonster)stateMachine).idleState);
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
