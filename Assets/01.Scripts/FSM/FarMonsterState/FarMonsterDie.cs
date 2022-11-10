using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���Ÿ� ���� ���� ���� ��ũ��Ʈ
/// </summary>
public class FarMonsterDie : BaseState
{
    BasicFarMonster monster;

    public FarMonsterDie(BasicFarMonster stateMachine) : base("Die", stateMachine)
    {
        monster = (BasicFarMonster)stateMachine;
    }

    // ���� ���� ��
    // ���� �ִϸ��̼� ����
    public override void Enter()
    {
        base.Enter();
        monster.anim.SetBool(monster.hashDie, true);
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
