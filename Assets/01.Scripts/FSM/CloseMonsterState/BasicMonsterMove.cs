using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.UIElements;
using System.Runtime.InteropServices.WindowsRuntime;
/// <summary>
/// �ٰŸ� ������ �̵� ��ũ��Ʈ
/// </summary>
public class BasicMonsterMove : BaseState
{
    BasicCloseMonster monster;
    Transform target;

    // Move ������
    public BasicMonsterMove(BasicCloseMonster stateMachine) : base("MOVE", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
    }

    #region DASH 
    private bool isDash = false;
    private bool CanDash => dashTime <= 0f;
    private float dashCoolTime = 2.0f;
    private float dashTime = 0;
    private float dashDistance = 25f;
    private float dashSpeed = 100f;

    private void CheckDash()
    {
        if (isDash) return;
        if (monster.distance >= dashDistance && CanDash)
        {
            Dash(monster.dir);
        }
    }

    private void CheckDashCoolTime()
    {
        if(isDash)
        {
            SetUseCoolTime();
        }
        else
        {
            if (CanDash) return;
            SetReadyCoolTime();
        }
    }

    private void SetUseCoolTime()
    {
        dashTime += Time.deltaTime;
        if (dashTime >= Define.DASH_DURATION)
        {
            StopDash();
        }
    }
    private void SetReadyCoolTime()
    {
        dashTime -= Time.deltaTime;
    }

    private void Dash(Vector3 velocity)
    {
        isDash = true;
        SetMove(false);

        monster.rigid.AddForce(velocity.normalized * dashSpeed, ForceMode.Impulse);

    }
    
    private void StopDash()
    {
        dashTime = dashCoolTime;
        monster.rigid.velocity = Vector3.zero;
        isDash = false;
        SetMove(true);
    }

    #endregion

    #region MOVE

    private void Move()
    {
        monster.LookTarget(target);
        monster.agent.SetDestination(target.position);
    }

    private void SetMove(bool isMove)
    {
        monster.agent.isStopped = !isMove;
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

    // �ٸ� STATE�� �Ѿ�� ����
    public override void CheckDistance()
    {
        base.CheckDistance();
        if (monster.agent.remainingDistance <= monster.agent.stoppingDistance)
        {
            stateMachine.ChangeState(monster.idleState);
        }
    }

    // NavMesh�� �̿��Ͽ� �̵�
    // �����ִٸ� isStopped=false
    // �ִϸ��̼� ����
    // Ÿ�� �Ѿư��� ����
    public override void Enter()
    {
        base.Enter();

        SetMove(true);
        SetAnim(true);
    }

    // Ÿ�� ��� ã���� �Ѿư��� + �Ĵٺ���
    // ���� �Ÿ��� ���� ���� ��ȯ
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        target = monster.SerachTarget();

        Move();
        CheckDashCoolTime();
        CheckDash();
    }

    // ���� ������ ��
    // �ִϸ��̼� ���� ������ ���߱�
    public override void Exit()
    {
        base.Exit();
        SetAnim(false);
        SetMove(false);
    }
    #endregion
}
