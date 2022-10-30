using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �ٰŸ� ���� ���� ���� ��ũ��Ʈ
/// </summary>
public class BasicMonsterDie : BaseState
{
    BasicCloseMonster monster;

    public BasicMonsterDie(BasicCloseMonster stateMachine) : base("Die", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
    }

    // ���� ���� ��
    // ���� �ִϸ��̼� ����
    public override void Enter()
    {
        base.Enter();
        monster.DieAnimation(true);
        ResetMonster();
    }

    private void ResetMonster()
    {
        // ���� �ʱ�ȭ �Լ�
    }

    // ���� ������ ��
    public override void Exit()
    {
        base.Exit();
    }
}
