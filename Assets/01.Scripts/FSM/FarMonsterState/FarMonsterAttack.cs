using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarMonsterAttack : BaseState
{
    BasicFarMonster monster;
    Transform target;

    float attackDelay = 3.0f;
    float nowAttackcool = 0.0f;

    public FarMonsterAttack(BasicFarMonster stateMachine) : base("ATTACK", stateMachine)
    {
        monster = (BasicFarMonster)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        nowAttackcool = 0;
        monster.anim.SetBool(monster.hashAttack, true);
    }

    void Shoot()
    {

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        target = monster.SerachTarget();

        if (monster.distance > monster.agent.stoppingDistance)
        {
            stateMachine.ChangeState(((BasicFarMonster)stateMachine).idleState);
        }
        if (nowAttackcool <= attackDelay)
        {
            nowAttackcool += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            stateMachine.ChangeState(((BasicFarMonster)stateMachine).damageState);
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
