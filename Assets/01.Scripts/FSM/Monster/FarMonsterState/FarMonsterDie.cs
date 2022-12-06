//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
///// <summary>
///// 원거리 몬스터 죽은 상태 스크립트
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
//        // 몬스터 초기화 함수
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
//    // 상태 시작 시
//    // 죽은 애니메이션 실행
//    public override void Enter()
//    {
//        base.Enter();
//        SetAnim(true);
//        ResetMonster();
//    }

//    // 상태 끝났을 시
//    public override void Exit()
//    {
//        base.Exit();
//    }

//    #endregion
//}
