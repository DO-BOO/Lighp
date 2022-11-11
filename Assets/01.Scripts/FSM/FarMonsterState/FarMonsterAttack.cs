using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
/// <summary>
/// 원거리 몬스터 공격 스크립트
/// 공격 함수는 BasicFarMonster 에 Shooting 있음
/// 여기서는 애니메이션 실행 해주는 역할만 해줌
/// </summary>
public class FarMonsterAttack : BaseState
{
    BasicFarMonster monster;
    Transform target;

    // 생성자
    public FarMonsterAttack(BasicFarMonster stateMachine) : base("ATTACK", stateMachine)
    {
        monster = (BasicFarMonster)stateMachine;
    }

    #region AVOID

    private const float avoidCoolTime = Define.AVOID_COOLTIME;
    private const float avoidDuration = Define.AVOID_DURATION;
    private const float avoidDistance = 25f;

    private bool CanAvoid => avoidTime <= 0f;
    private bool isAvoid = false;

    private float avoidTime = 0;
    private float avoidSpeed = 100f;

    private void CheckAvoid()
    {
        if (isAvoid) return;
        if (monster.distance <= avoidDistance && CanAvoid)
        {
            Avoid(monster.dir * -1);
        }
    }

    private void CheckAvoidCoolTime()
    {
        if (isAvoid)
        {
            SetUseCoolTime();
        }
        else
        {
            if (CanAvoid) return;
            SetReadyCoolTime();
        }
    }

    private void SetUseCoolTime()
    {
        avoidTime += Time.deltaTime;
        if (avoidTime >= avoidDuration)
        {
            StopAvoid();
        }
    }
    private void SetReadyCoolTime()
    {
        avoidTime -= Time.deltaTime;
    }

    private void Avoid(Vector3 velocity)
    {
        isAvoid = true;
        int randDir = Random.Range(0, 100);
        Vector3 addTurnDir = Vector3.zero;

        // 뒤로는 가지는데 각도를 좀 더 더해줘야할 듯
        // 보는 각도를 계속 바꿔주고 Vector3.back을 해주고
        // 각도를 조금 더 틀어주면 ㅇㅋ

        monster.rigid.AddForce((velocity.normalized) * avoidSpeed, ForceMode.Impulse);
    }

    private void StopAvoid()
    {
        avoidTime = avoidCoolTime;
        monster.rigid.velocity = Vector3.zero;
        isAvoid = false;
    }

    #endregion

    #region ANIMATION

    public override void SetAnim(bool isPlay)
    {
        base.SetAnim(isPlay);
        monster.AttackAnimation(isPlay);
    }

    #endregion

    #region STATE

    public override void CheckDistance()
    {
        base.CheckDistance();
        if (monster.distance > monster.attackRange)
        {
            stateMachine.ChangeState(monster.idleState);
        }
    }

    // 상태 시작 시
    // 애니메이션 실행
    public override void Enter()
    {
        base.Enter();
        SetAnim(true);
    }

    // Attack Animation에 이벤트로 추가해서 애니메이션 박자에 맞게 쏘도록 함
    // 거리가 멀어지면 상태 전환 + 타겟 쳐다보기
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        target = monster.SerachTarget();
        monster.LookTarget(target);

        CheckAvoid();
        CheckAvoidCoolTime();
    }

    // 상태 끝났을 시
    // 애니메이션 끄기
    public override void Exit()
    {
        base.Exit();
        SetAnim(false);
    }

    #endregion

}
