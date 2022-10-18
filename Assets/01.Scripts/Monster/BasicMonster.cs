using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Monster�� �⺻ FSM
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

    // �⺻ State ��������
    protected override BaseState GetInitState()
    {
        return idleState;
    }
}
