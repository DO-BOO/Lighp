using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.ShaderGraph.Internal;
using UnityEditorInternal;
using UnityEngine;
/// <summary>
/// 근거리 몬스터가 데미지 받은 상태의 스크립트
/// </summary>
public class BasicMonsterDamage : BaseState
{
    BasicCloseMonster monster;

    // 데미지 생성자
    public BasicMonsterDamage(BasicCloseMonster stateMachine) : base("DAMAGED", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
    }

    #region DAMAGE
    float delayTime = 1.0f;
    float nowDelay = 0.0f;
    float damage = 20f;

    private void SetDelay(float delay)
    {
        nowDelay = delay;
    }

    #endregion

    #region ANIMATION

    public override void SetAnim()
    {
        base.SetAnim();
        monster.DamageAnimation();
    }

    #endregion

    #region STATE

    public override void CheckDistance()
    {
        base.CheckDistance();
        if (monster.GetHP <= 0)
        {
            stateMachine.ChangeState(monster.dieState);
        }
        if (nowDelay >= delayTime)
        {
            stateMachine.ChangeState(monster.idleState);
        }

    }

    // 상태 시작 시
    // 피해량 만큼 데미지 감소 + 애니메이션 실행
    // 만약 HP가 0 이하가 되면 상태 Die로 변환
    public override void Enter()
    {
        base.Enter();
        SetDelay(0);
        monster.SetHP(false, damage);
        SetAnim();
    }

    // 일정 시간이 지나면 상태 변환
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        nowDelay += Time.deltaTime;
    }

    // 상태 끝났을 시
    public override void Exit()
    {
        base.Exit();
    }

    #endregion
}
