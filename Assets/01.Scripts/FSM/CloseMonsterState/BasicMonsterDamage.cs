using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditorInternal;
using UnityEngine;

public class BasicMonsterDamage : BaseState
{
    BasicCloseMonster monster;

    float delayTime = 1.0f;
    float nowDelay = 0.0f;

    float HP = 100f;
    float damage = 20f;

    public BasicMonsterDamage(BasicCloseMonster stateMachine) : base("DAMAGED", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        nowDelay = 0;
        HP -= damage;
        if (HP <= 0)
        {
            stateMachine.ChangeState(((BasicCloseMonster)stateMachine).dieState);
        }
        monster.anim.SetTrigger(monster.hashDamage);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        nowDelay += Time.deltaTime;
        if (nowDelay >= delayTime)
        {
            stateMachine.ChangeState(((BasicCloseMonster)stateMachine).idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
