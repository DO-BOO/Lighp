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
