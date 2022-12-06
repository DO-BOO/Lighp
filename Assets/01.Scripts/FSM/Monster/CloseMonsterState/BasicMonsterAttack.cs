using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �ٰŸ� ���� ���� ���� ��ũ��Ʈ
/// </summary>
public class BasicMonsterAttack : BaseState
{
    BasicCloseMonster monster;
    Transform target;

    // ���� �̿�
    float attackDelay = 2.0f; // ������ �� ����
    float nowAttackcool = 0.0f; // ���� ��Ÿ��
    float attackSpeed = 3.0f; // ���� �ӵ�

    // ���� ���� ������
    public BasicMonsterAttack(BasicCloseMonster stateMachine) : base("ATTACK", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
    }

    #region STATE

    public override void CheckDistance()
    {
        base.CheckDistance();
        if (monster.distance > monster.attackRange)
        {
            stateMachine.ChangeState(monster.idleState);
        }
    }

    // ���� ���� ��
    // ���� �ִϸ��̼� ����
    public override void Enter()
    {
        base.Enter();
        SetAnim(true);
    }

    // ���͸� �ٶ󺸸� ���� �Ÿ��� �ٽ� �־����� ���� �ٽ� ����
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        target = monster.SerachTarget();
        monster.LookTarget(target);
    }

    // ���°� ������ ��
    // �ִϸ��̼ǵ� ����
    public override void Exit()
    {
        base.Exit();
        SetAnim(false);
    }
    #endregion

}
