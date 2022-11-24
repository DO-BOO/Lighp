using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.UIElements;
using System.Runtime.InteropServices.WindowsRuntime;
/// <summary>
/// 근거리 몬스터의 이동 스크립트
/// </summary>
public class BasicMonsterMove : BaseState
{
    BasicCloseMonster monster;
    Transform target;

    // Move 생성자
    public BasicMonsterMove(BasicCloseMonster stateMachine) : base("MOVE", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
    }

    #region DASH 
    private const float COOLTIME = Define.DASH_COOLTIME;
    private const float DURATION = Define.DASH_DURATION;
    private const float DISTANCE = Define.DASH_DISTANCE;

    private bool CanDash => dashCoolTimer <= 0f;
    private bool isDash = false;
    private float dashCoolTimer = 0;

    private void CheckDash()
    {
        if (isDash) return;
        if (monster.distance >= DISTANCE && CanDash)
        {
            isDash = true;
            SetMove(false);

            monster.WarningDash(monster.dir.normalized);
            Dash(monster.dir);
        }
    }
    private void CheckDashCoolTime()
    {
        if (dashCoolTimer > 0)
        {
            dashCoolTimer -= Time.deltaTime;
        }
    }

    private void Dash(Vector3 velocity)
    {
        Vector3 destination = monster.transform.position + velocity.normalized * DISTANCE;

        monster.transform.DOKill();
        monster.transform.DOMove(destination, DURATION).OnComplete(() => { OnEndDash(); });
    }

    private void OnEndDash()
    {
        isDash = false;
        SetMove(true);
        dashCoolTimer = COOLTIME;
    }

    #endregion

    #region MOVE

    bool stopMove = false;
    private void Move()
    {
        if (target == null || stopMove) return;
        monster.LookTarget(target);
        monster.agent.SetDestination(target.position);
    }

    private void SetMove(bool isMove)
    {
        stopMove = !isMove;
    }

    #endregion

    #region ANIMATION

    public override void SetAnim(bool isPlay)
    {
        base.SetAnim();
        monster.MoveAnimation(isPlay);
    }

    #endregion

    #region STATE

    // 다른 STATE로 넘어가는 조건
    public override void CheckDistance()
    {
        base.CheckDistance();
        if (monster.distance <= monster.attackRange)
        {
            stateMachine.ChangeState(monster.idleState);
        }
    }

    // NavMesh를 이용하여 이동
    // 멈춰있다면 isStopped=false
    // 애니메이션 실행
    // 타겟 쫓아가기 시작
    public override void Enter()
    {
        base.Enter();

        SetMove(true);
        SetAnim(true);
    }

    // 타겟 계속 찾으며 쫓아가기 + 쳐다보기
    // 남은 거리에 따라 상태 전환
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        target = monster.SerachTarget();

        Move();
        CheckDashCoolTime();
        CheckDash();
    }

    // 상태 끝났을 시
    // 애니메이션 끄고 움직임 멈추기
    public override void Exit()
    {
        base.Exit();
        SetAnim(false);
        SetMove(false);
    }

    #endregion

}
