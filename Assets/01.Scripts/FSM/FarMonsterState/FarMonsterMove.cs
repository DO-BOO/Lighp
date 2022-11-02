using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 원거리 몬스터 이동 스크립트
/// </summary>
public class FarMonsterMove : BaseState
{
    BasicFarMonster monster;
    Transform target;

    // Move 생성자
    public FarMonsterMove(BasicFarMonster stateMachine) : base("MOVE", stateMachine)
    {
        monster = (BasicFarMonster)stateMachine;
    }

    // NavMesh를 이용하여 이동
    // 멈춰있다면 isStopped=false
    // 애니메이션 실행
    // 타겟 쫓아가기 시작
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

    // 타겟 계속 찾으며 쫓아가기 + 쳐다보기
    // 남은 거리에 따라 상태 전환
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

    // 상태 끝났을 시
    // 애니메이션 끄고 움직임 멈추기
    public override void Exit()
    {
        base.Exit();
        monster.anim.SetBool(monster.hashWalk, false);
        monster.agent.isStopped = true;
    }


}
