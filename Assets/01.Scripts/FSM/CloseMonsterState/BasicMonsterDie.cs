using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMonsterDie : BaseState
{
    BasicCloseMonster monster;

    public BasicMonsterDie(BasicCloseMonster stateMachine) : base("Die", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        monster.DieAnimation(true);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
