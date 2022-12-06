using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// (�⺻) �ٰŸ� ���� IDLE ���� 
/// </summary>
public class BasicMonsterIdle : BaseState
{
    Monster monster;
    Transform target = null;

    Dictionary<BaseState, float> checkDic;

    // IDLE ���� ����
    public BasicMonsterIdle(Monster stateMachine, Dictionary<BaseState, float> checkDic) : base("IDLE", stateMachine)
    {
        monster = (Monster)stateMachine;
        this.checkDic = checkDic;
        checkDic.OrderBy(x => x.Value);
    }

    #region STATE

    public override void CheckDistance()
    {
        base.CheckDistance();
        foreach (var state in checkDic.Keys)
        {
            if (monster.distance <= checkDic[state])
            {
                stateMachine.ChangeState(state);
            }
        }
        
        if (monster.distance <= monster.moveRange)
        {
            stateMachine.ChangeState(monster.states[typeof(BasicMonsterMove)]);
        }
    }

    // ���� ���� ��
    // Ÿ�� ã�� ����
    public override void Enter()
    {
        base.Enter();
    }

    // Ÿ���� �ִٸ�
    // �Ÿ��� ���� ���� ��ȯ
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        target = monster.SerachTarget();
    }

    // ���� ������ ��
    public override void Exit()
    {
        base.Exit();
    }

    #endregion
}
