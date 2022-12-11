//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.EditorTools;
//using UnityEngine;
///// <summary>
///// 원거리 몬스터 공격 스크립트
///// 공격 함수는 BasicFarMonster 에 Shooting 있음
///// 여기서는 애니메이션 실행 해주는 역할만 해줌
///// </summary>
//public class FarMonsterAttack : BaseState
//{
//    BasicFarMonster monster;
//    Transform target;

//    // 생성자
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

//    // 상태 시작 시
//    // 애니메이션 실행
//    public override void Enter()
//    {
//        base.Enter();
//        SetAnim(true);
//    }

//    // Attack Animation에 이벤트로 추가해서 애니메이션 박자에 맞게 쏘도록 함
//    // 거리가 멀어지면 상태 전환 + 타겟 쳐다보기
//    public override void UpdateLogic()
//    {
//        base.UpdateLogic();
//        target = monster.SerachTarget();
//        monster.LookTarget(target);

//        CheckAvoid();
//        CheckAvoidCoolTime();
//    }

//    // 상태 끝났을 시
//    // 애니메이션 끄기
//    public override void Exit()
//    {
//        base.Exit();
//        SetAnim(false);
//    }

//    #endregion

//}
