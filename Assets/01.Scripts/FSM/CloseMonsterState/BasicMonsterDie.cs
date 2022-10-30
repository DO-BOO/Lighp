using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 근거리 몬스터 죽은 상태 스크립트
/// </summary>
public class BasicMonsterDie : BaseState
{
    BasicCloseMonster monster;

    public BasicMonsterDie(BasicCloseMonster stateMachine) : base("Die", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
    }

    // 상태 시작 시
    // 죽은 애니메이션 실행
    public override void Enter()
    {
        base.Enter();
        monster.DieAnimation(true);
        ResetMonster();
    }

    private void ResetMonster()
    {
        // 몬스터 초기화 함수
    }

    // 상태 끝났을 시
    public override void Exit()
    {
        base.Exit();
    }
}
