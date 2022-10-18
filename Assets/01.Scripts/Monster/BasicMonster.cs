using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Monster�� �⺻ FSM
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

    // �⺻ State ��������
    protected override BaseState GetInitState()
    {
        return idleState;
    }
}
