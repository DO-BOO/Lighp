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
    private const float dashCoolTime = Define.DASH_COOLTIME;
    private const float dashDuration = Define.DASH_DURATION;
    private const float dashDistance = 25f;

    private bool CanDash => dashTime <= 0f;
    private bool isDash = false;
    
    private float dashSpeed = 100f;
    private float dashTime = 0;

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
        if (dashTime >= dashDuration)
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
        monster.rigid.velocity = Vector3.zero;
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
        if (target == null) return;
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
        if (monster.distance <= monster.attackRange)
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
