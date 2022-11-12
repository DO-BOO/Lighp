using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarMonsterStun : BaseState
{
    BasicFarMonster monster;

    // 데미지 생성자
    public FarMonsterStun(BasicFarMonster stateMachine) : base("STUN", stateMachine)
    {
        monster = (BasicFarMonster)stateMachine;
    }

    #region VARIABLE
    private float stunTime = 3f;
    private float curTime = 0;
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
        SetAnim(true);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        curTime += Time.deltaTime;
        if(curTime >= stunTime)
        {
            monster.ChangeState(monster.moveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        SetAnim(false);
    }

    #endregion

}
