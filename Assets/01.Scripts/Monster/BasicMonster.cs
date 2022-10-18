using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Monster의 기본 FSM
/// </summary>
public class BasicMonster : StateMachine
{
    [HideInInspector]
    public BasicMonsterIdle idleState;
    public BasicMonsterMove moveState;

    private void Awake()
    {
        idleState = new BasicMonsterIdle(this);
        moveState = new BasicMonsterMove(this);
    }

    // 기본 State 가져오기
    protected override BaseState GetInitState()
    {
        return idleState;
    }
}
