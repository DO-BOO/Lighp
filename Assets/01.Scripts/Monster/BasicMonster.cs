using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Monster의 기본 FSM
/// </summary>
public class BasicMonster : StateMachine
{
    public GameObject target;
    [HideInInspector]
    public BasicMonsterIdle idleState;
    public BasicMonsterMove moveState;

    public NavMeshAgent agent; 

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        idleState = new BasicMonsterIdle(this);
        moveState = new BasicMonsterMove(this);
    }

    // 기본 State 가져오기
    protected override BaseState GetInitState()
    {
        return idleState;
    }
}
