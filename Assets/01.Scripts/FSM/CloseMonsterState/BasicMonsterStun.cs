using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMonsterStun : BaseState
{
    BasicCloseMonster monster;

    // 데미지 생성자
    public BasicMonsterStun(BasicCloseMonster stateMachine) : base("STUN", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
    }

    #region VARIABLE
    private float stunTime = 3f;
    private float curTime = 0;

    private void InitState()
    {
        curTime = 0;
    }
    #endregion

    #region ANIMATION

    public override void SetAnim(bool isPlay)
    {
        base.SetAnim(isPlay);
        monster.StunAnimation(isPlay);
    }
    #endregion

    #region STATE
   
    public override void Enter()
    {
        base.Enter();
        InitState();
        monster.SetStun(true);
        SetAnim(true);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        curTime += Time.deltaTime;
        if (curTime >= stunTime)
        {
            monster.ChangeState(monster.moveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        monster.SetStun(false);
        SetAnim(false);
    }

    #endregion
}
