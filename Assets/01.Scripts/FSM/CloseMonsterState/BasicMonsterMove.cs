using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
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

    #region MOVE

    const float monsterSpeed = 10.0f;

    private void Move()
    {
        if (target == null) return;
        monster.LookTarget(target);
        monster.agent.SetDestination(target.position);
    }

    private void SetMove(bool isMove)
    {
        if (!isMove)
        {
            monster.agent.speed = 0;
            monster.agent.velocity = Vector3.zero;
        }
        else
        {
            monster.agent.speed = monsterSpeed;
        }
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

    float DISTANCE = 20f;

    // �ٸ� STATE�� �Ѿ�� ����
    public override void CheckDistance()
    {
        base.CheckDistance();
        if (monster.distance <= monster.attackRange)
        {
            stateMachine.ChangeState(monster.idleState);
        }
        else if (monster.distance >= DISTANCE && monster.canDash)
        {
            stateMachine.ChangeState(monster.dashState);
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
