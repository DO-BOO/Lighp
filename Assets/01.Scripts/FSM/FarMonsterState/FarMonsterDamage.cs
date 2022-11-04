using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 원거리 몬스터 데미지 스크립트
/// </summary>
public class FarMonsterDamage : BaseState
{
    BasicFarMonster monster;

    float delayTime = 1.0f; // 무적 시간
    float nowDelay = 0.0f;

    float HP = 100f; // 체력
    float damage = 20f; // 데미지 입을 크기

    // 생성자
    public FarMonsterDamage(BasicFarMonster stateMachine) : base("DAMAGED", stateMachine)
    {
        monster = (BasicFarMonster)stateMachine;
    }

    // 상태 시작 시
    // 피해량 만큼 데미지 감소 + 애니메이션 실행
    // 만약 HP가 0 이하가 되면 상태 Die로 변환
    public override void Enter()
    {
        base.Enter();
        nowDelay = 0;
        HP -= damage;
        if (HP <= 0)
        {
            stateMachine.ChangeState(((BasicFarMonster)stateMachine).dieState);
        }
        monster.anim.SetTrigger(monster.hashDamage);
    }

    // 일정 시간이 지나면 상태 변환
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        nowDelay += Time.deltaTime;
        if (nowDelay >= delayTime)
        {
            stateMachine.ChangeState(((BasicFarMonster)stateMachine).idleState);
        }

    }

    // 상태 끝났을 시
    public override void Exit()
    {
        base.Exit();
    }
}
