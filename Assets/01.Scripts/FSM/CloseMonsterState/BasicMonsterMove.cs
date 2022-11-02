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
    // Move ������
    public BasicMonsterMove(BasicCloseMonster stateMachine) : base("MOVE", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
    }

    // NavMesh�� �̿��Ͽ� �̵�
    // �����ִٸ� isStopped=false
    // �ִϸ��̼� ����
    // Ÿ�� �Ѿư��� ����
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

    // Ÿ�� ��� ã���� �Ѿư��� + �Ĵٺ���
    // ���� �Ÿ��� ���� ���� ��ȯ
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

    // ���� ������ ��
    // �ִϸ��̼� ���� ������ ���߱�
    public override void Exit()
    {
        base.Exit();
        monster.MoveAnimation(false);
        monster.agent.isStopped = true;
    }
    #endregion
}
