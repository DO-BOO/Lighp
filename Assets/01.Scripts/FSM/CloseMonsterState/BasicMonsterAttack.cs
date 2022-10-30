using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 근거리 몬스터 공격 상태 스크립트
/// </summary>
public class BasicMonsterAttack : BaseState
{
    BasicCloseMonster monster;
    Transform target;

    // 아직 미완
    float attackDelay = 2.0f; // 딜레이 줄 변수
    float nowAttackcool = 0.0f; // 현재 쿨타임
    float attackSpeed = 3.0f; // 공격 속도

    // 어택 상태 생성자
    public BasicMonsterAttack(BasicCloseMonster stateMachine) : base("ATTACK", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
    }

    // 상태 시작 시
    // 공격 애니메이션 시작
    public override void Enter()
    {
        base.Enter();
        monster.AttackAnimation(true);
    }

    // 몬스터를 바라보며 만약 거리가 다시 멀어지면 상태 다시 선택
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        target = monster.SerachTarget();

        monster.LookTarget(target);
        if (monster.distance > monster.agent.stoppingDistance)
        {
            stateMachine.ChangeState(((BasicCloseMonster)stateMachine).idleState);
        }
    }

    // 상태가 끝났을 시
    // 애니메이션도 끄기
    public override void Exit()
    {
        base.Exit();
        monster.AttackAnimation(false);
    }


}
