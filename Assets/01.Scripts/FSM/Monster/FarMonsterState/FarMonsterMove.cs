//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
///// <summary>
///// ���Ÿ� ���� �̵� ��ũ��Ʈ
///// </summary>
//public class FarMonsterMove : BaseState
//{
//    BasicFarMonster monster;
//    Transform target;

//    // Move ������
//    public FarMonsterMove(BasicFarMonster stateMachine) : base("MOVE", stateMachine)
//    {
//        monster = (BasicFarMonster)stateMachine;
//    }


//    #region MOVE

//    private void Move()
//    {
//        if (target == null) return;
//        monster.LookTarget(target);
//        monster.agent.SetDestination(target.position);
//    }
//    private void SetMove(bool isMove)
//    {
//        monster.agent.isStopped = !isMove;
//    }

//    #endregion

//    #region ANIMATION

//    public override void SetAnim(bool isPlay)
//    {
//        base.SetAnim();
//        monster.MoveAnimation(isPlay);
//    }

//    #endregion

//    #region STATE

//    // �ٸ� STATE�� �Ѿ�� ����
//    public override void CheckDistance()
//    {
//        base.CheckDistance();
//        if (monster.distance <= monster.attackRange)
//        {
//            stateMachine.ChangeState(monster.idleState);
//        }
//    }

//    // NavMesh�� �̿��Ͽ� �̵�
//    // �����ִٸ� isStopped=false
//    // �ִϸ��̼� ����
//    // Ÿ�� �Ѿư��� ����
//    public override void Enter()
//    {
//        base.Enter();

//        SetMove(true);
//        SetAnim(true);
//    }

//    // Ÿ�� ��� ã���� �Ѿư��� + �Ĵٺ���
//    // ���� �Ÿ��� ���� ���� ��ȯ
//    public override void UpdateLogic()
//    {
//        base.UpdateLogic();
//        target = monster.SerachTarget();

//        Move();
//    }

//    // ���� ������ ��
//    // �ִϸ��̼� ���� ������ ���߱�
//    public override void Exit()
//    {
//        base.Exit();
//        SetAnim(false);
//        SetMove(false);
//    }
//    #endregion
//}
