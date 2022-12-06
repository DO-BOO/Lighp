using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// (기본) 근거리 몬스터 IDLE 상태 
/// </summary>
public class BasicMonsterIdle : BaseState
{
    Monster monster;
    Transform target = null;

    Dictionary<BaseState, float> checkDic;

    // IDLE 상태 정의
    public BasicMonsterIdle(Monster stateMachine, Dictionary<BaseState, float> checkDic) : base("IDLE", stateMachine)
    {
        monster = (Monster)stateMachine;
        this.checkDic = checkDic;
        checkDic.OrderBy(x => x.Value);
    }

    #region STATE

    public override void CheckDistance()
    {
        base.CheckDistance();
        foreach (var state in checkDic.Keys)
        {
            if (monster.distance <= checkDic[state])
            {
                stateMachine.ChangeState(state);
            }
        }
        
        if (monster.distance <= monster.moveRange)
        {
            stateMachine.ChangeState(monster.states[typeof(BasicMonsterMove)]);
        }
    }

    // 상태 시작 시
    // 타겟 찾기 시작
    public override void Enter()
    {
        base.Enter();
    }

    // 타겟이 있다면
    // 거리에 따라 상태 전환
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        target = monster.SerachTarget();
    }

    // 상태 끝났을 시
    public override void Exit()
    {
        base.Exit();
    }

    #endregion
}
