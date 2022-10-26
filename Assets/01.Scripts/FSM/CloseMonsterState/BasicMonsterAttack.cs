using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMonsterAttack : BaseState
{
    BasicCloseMonster monster;
    Transform target;

    float attackDelay = 3.0f;
    float nowAttackcool = 0.0f;

    public BasicMonsterAttack(BasicCloseMonster stateMachine) : base("ATTACK", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        nowAttackcool = 0;
        monster.anim.SetBool(monster.hashAttack, true);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        target = monster.SerachTarget();

        if(monster.distance > monster.agent.stoppingDistance)
        {
            stateMachine.ChangeState(((BasicCloseMonster)stateMachine).idleState);
        }
        if (nowAttackcool <= attackDelay)
        {
            nowAttackcool += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            stateMachine.ChangeState(((BasicCloseMonster)stateMachine).damageState);
        }
    }

    public override void UpdateLate()
    {
        base.UpdateLogic();
    }

    public override void Exit()
    {
        base.Exit();
        monster.anim.SetBool(monster.hashAttack, false);
    }

}
