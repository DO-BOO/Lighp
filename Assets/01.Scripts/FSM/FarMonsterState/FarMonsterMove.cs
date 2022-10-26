using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarMonsterMove : BaseState
{
    BasicFarMonster monster;
    Transform target;


    public FarMonsterMove(BasicFarMonster stateMachine) : base("MOVE", stateMachine)
    {
        monster = (BasicFarMonster)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        monster.agent.isStopped = false;

        monster.anim.SetBool(monster.hashWalk, true);

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
            stateMachine.ChangeState(((BasicFarMonster)stateMachine).idleState);
        }

    }

    public override void UpdateLate()
    {
        base.UpdateLogic();
        if (Input.GetKeyDown(KeyCode.E))
        {
            stateMachine.ChangeState(((BasicFarMonster)stateMachine).damageState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        monster.anim.SetBool(monster.hashWalk, false);
        monster.agent.isStopped = true;
    }
}
