using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
/// <summary>
/// ���Ÿ� ���� ���� ��ũ��Ʈ
/// ���� �Լ��� BasicFarMonster �� Shooting ����
/// ���⼭�� �ִϸ��̼� ���� ���ִ� ���Ҹ� ����
/// </summary>
public class FarMonsterAttack : BaseState
{
    BasicFarMonster monster;
    Transform target;

    //float attackDelay = 3.0f;
    //float nowAttackcool = 0.0f;

    // ������
    public FarMonsterAttack(BasicFarMonster stateMachine) : base("ATTACK", stateMachine)
    {
        monster = (BasicFarMonster)stateMachine;
    }

    // ���� ���� ��
    // �ִϸ��̼� ����
    public override void Enter()
    {
        base.Enter();
        monster.anim.SetBool(monster.hashAttack, true);
    }

    // Attack Animation�� �̺�Ʈ�� �߰��ؼ� �ִϸ��̼� ���ڿ� �°� ��� ��
    // �Ÿ��� �־����� ���� ��ȯ + Ÿ�� �Ĵٺ���
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        target = monster.SerachTarget();

        if (monster.distance > monster.agent.stoppingDistance)
        {
            stateMachine.ChangeState(((BasicFarMonster)stateMachine).idleState);
        }
        if(target) monster.LookTarget(target);
    }

    // ���� ������ ��
    // �ִϸ��̼� ����
    public override void Exit()
    {
        base.Exit();
        monster.anim.SetBool(monster.hashAttack, false);
    }
}
