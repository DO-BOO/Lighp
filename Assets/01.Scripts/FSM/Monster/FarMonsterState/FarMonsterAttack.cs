//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.EditorTools;
//using UnityEngine;
///// <summary>
///// ���Ÿ� ���� ���� ��ũ��Ʈ
///// ���� �Լ��� BasicFarMonster �� Shooting ����
///// ���⼭�� �ִϸ��̼� ���� ���ִ� ���Ҹ� ����
///// </summary>
//public class FarMonsterAttack : BaseState
//{
//    BasicFarMonster monster;
//    Transform target;

//    // ������
//    public FarMonsterAttack(BasicFarMonster stateMachine) : base("ATTACK", stateMachine)
//    {
//        monster = (BasicFarMonster)stateMachine;
//    }


//    #region ANIMATION

//    public override void SetAnim(bool isPlay)
//    {
//        base.SetAnim(isPlay);
//        monster.AttackAnimation(isPlay);
//    }

//    #endregion

//    #region STATE

//    public override void CheckDistance()
//    {
//        base.CheckDistance();
//        if (monster.distance > monster.attackRange)
//        {
//            stateMachine.ChangeState(monster.idleState);
//        }
//    }

//    // ���� ���� ��
//    // �ִϸ��̼� ����
//    public override void Enter()
//    {
//        base.Enter();
//        SetAnim(true);
//    }

//    // Attack Animation�� �̺�Ʈ�� �߰��ؼ� �ִϸ��̼� ���ڿ� �°� ��� ��
//    // �Ÿ��� �־����� ���� ��ȯ + Ÿ�� �Ĵٺ���
//    public override void UpdateLogic()
//    {
//        base.UpdateLogic();
//        target = monster.SerachTarget();
//        monster.LookTarget(target);

//        CheckAvoid();
//        CheckAvoidCoolTime();
//    }

//    // ���� ������ ��
//    // �ִϸ��̼� ����
//    public override void Exit()
//    {
//        base.Exit();
//        SetAnim(false);
//    }

//    #endregion

//}
