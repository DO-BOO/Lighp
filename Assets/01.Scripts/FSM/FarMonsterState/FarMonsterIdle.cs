using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// (기본) 원거리 몬스터 IDLE 상태 
/// </summary>
public class FarMonsterIdle : BaseState
{
    BasicFarMonster monster;
    Transform target = null;

    // IDLE 상태 정의
    public FarMonsterIdle(BasicFarMonster stateMachine) : base("IDLE", stateMachine)
    {
        monster = (BasicFarMonster)stateMachine;
    }

    // 상태 시작 시
    // 타겟 찾기 시작
    public override void Enter()
    {
        base.Enter();
        target = monster.SerachTarget();
    }

    // 타겟이 있다면
    // 거리에 따라 상태 전환
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        target = monster.SerachTarget();

        if (target)
        {
            if (monster.distance <= monster.attackRange)
            {
                stateMachine.ChangeState(((BasicFarMonster)stateMachine).attackState);
            }
            else if (monster.distance >= monster.moveRange)
            {
                stateMachine.ChangeState(((BasicFarMonster)stateMachine).moveState);
            }
        }
    }

    // 물리 연산을 위한 LateUpdate
    public override void UpdateLate()
    {
        base.UpdateLogic();
    }

    // 상태 끝났을 시
    public override void Exit()
    {
        base.Exit();
    }
}
