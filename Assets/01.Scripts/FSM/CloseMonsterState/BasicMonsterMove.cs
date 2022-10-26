using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicMonsterMove : BaseState
{
    BasicCloseMonster monster;
    Transform target;


    public BasicMonsterMove(BasicCloseMonster stateMachine) : base("MOVE", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        monster.agent.isStopped = false;

        monster.MoveAnimation(true);

        target = monster.SerachTarget();
        if (target)
        {
            monster.agent.SetDestination(target.position);
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        target = monster.SerachTarget();

        monster.agent.SetDestination(target.position);
        if (monster.agent.remainingDistance <= monster.agent.stoppingDistance)
        {
            stateMachine.ChangeState(((BasicCloseMonster)stateMachine).idleState);
        }
        
    }

    public override void UpdateLate()
    {
        base.UpdateLogic();
        //   stateMachine.ChangeState(((BasicCloseMonster)stateMachine).damageState);
    }
    public override void Exit()
    {
        base.Exit();
        monster.MoveAnimation(false);
        monster.agent.isStopped=true;
    }
}
