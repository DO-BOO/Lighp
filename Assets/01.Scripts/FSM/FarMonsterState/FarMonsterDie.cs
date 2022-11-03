using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarMonsterDie : BaseState
{
    BasicFarMonster monster;

    public FarMonsterDie(BasicFarMonster stateMachine) : base("Die", stateMachine)
    {
        monster = (BasicFarMonster)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        monster.anim.SetBool(monster.hashDie, true);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
