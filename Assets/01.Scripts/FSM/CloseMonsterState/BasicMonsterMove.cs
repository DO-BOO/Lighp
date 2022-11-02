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

    #region DASH 
    private bool isDash = false;
    private float dashCoolTime = -1;
    private float dashTime = 0.01f;
    private float dashDistance = 20f;

    private void Dash()
    {
        isDash = true;
        // monster.agent.isStopped = true;

        Debug.Log("Dash__Monster");
        monster.rigid.AddRelativeForce(monster.dir, ForceMode.VelocityChange);

        //StopDash();
    }

    //private void StopDash()
    //{
    //    isDash = false;
    //    monster.agent.isStopped = false;
    //}

    #endregion

    #region MOVE
    // Move 생성자
    public BasicMonsterMove(BasicCloseMonster stateMachine) : base("MOVE", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
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
        monster.LookTarget(target);

        if (isDash) return;

        monster.agent.SetDestination(target.position);
        if (monster.agent.remainingDistance <= monster.agent.stoppingDistance)
        {
            stateMachine.ChangeState(((BasicCloseMonster)stateMachine).idleState);
        }

        if(monster.distance >= dashDistance && !isDash)
        {
            Dash();
        }


    }

    // 상태 끝났을 시
    // 애니메이션 끄고 움직임 멈추기
    public override void Exit()
    {
        base.Exit();
        monster.MoveAnimation(false);
        monster.agent.isStopped = true;
    }
    #endregion
}
