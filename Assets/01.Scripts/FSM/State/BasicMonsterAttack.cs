using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMonsterAttack : BaseState
{
    BasicMonster monster;

    float attackDelay = 3.0f;
    float nowAttackcool = 0.0f;

    public BasicMonsterAttack(BasicMonster stateMachine) : base("ATTACK", stateMachine)
    {
        monster = (BasicMonster)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        nowAttackcool = 0;
        // ATTACK 애니메이션 실행
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (nowAttackcool <= attackDelay)
        {
            nowAttackcool += Time.deltaTime;
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
