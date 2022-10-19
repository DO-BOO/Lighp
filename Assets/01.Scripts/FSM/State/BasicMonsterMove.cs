using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicMonsterMove : BaseState
{
    BasicMonster monster;

    private float stopDistance = 7f;

    public BasicMonsterMove(BasicMonster stateMachine) : base("MOVE", stateMachine)
    {
        monster = (BasicMonster)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        monster.agent.SetDestination(monster.Target.transform.position);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        Debug.Log("ASD");

        if(monster.agent.remainingDistance <= stopDistance)
        {
            stateMachine.ChangeState(((BasicMonster)stateMachine).attackState);
        }

        monster.agent.SetDestination(monster.Target.transform.position);
    }
    public override void UpdateLate()
    {
        base.UpdateLogic();
    }
    public override void Exit()
    {
        base.Exit();
        monster.agent.ResetPath();
    }
}
