//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
///// <summary>
///// ���Ÿ� ���� ���� ���� ��ũ��Ʈ
///// </summary>
//public class FarMonsterDie : BaseState
//{
//    BasicFarMonster monster;

//    public FarMonsterDie(BasicFarMonster stateMachine) : base("Die", stateMachine)
//    {
//        monster = (BasicFarMonster)stateMachine;
//    }

//    #region DIE

//    private void ResetMonster()
//    {
//        // ���� �ʱ�ȭ �Լ�
//        Debug.Log("Far Die");
//        monster.gameObject.SetActive(false);
//    }

//    #endregion

//    #region ANIMATION

//    public override void SetAnim(bool isPlay)
//    {
//        base.SetAnim(isPlay);
//        monster.DieAnimation(isPlay);
//    }

//    #endregion

//    #region STATE
//    // ���� ���� ��
//    // ���� �ִϸ��̼� ����
//    public override void Enter()
//    {
//        base.Enter();
//        SetAnim(true);
//        ResetMonster();
//    }

//    // ���� ������ ��
//    public override void Exit()
//    {
//        base.Exit();
//    }

//    #endregion
//}
