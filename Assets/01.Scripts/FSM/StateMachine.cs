using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using UnityEngine;
/// <summary>
/// FSM ����
/// </summary>
public class StateMachine : MonoBehaviour
{
    BaseState curState; // ���� ����

    private void Start()
    {
        // �⺻ ���� ��������
        curState = GetInitState();  

        // �⺻ ���°� �ִٸ� ����
        if(curState != null)
        {
            curState.Enter();
        }
    }

    // �⺻ ���� null
    protected virtual BaseState GetInitState()
    {
        return null;
    }

    private void Update()
    {
        Debug.Log(curState);
        // State Update ���ֱ�
        if(curState !=null)
        {
            curState.UpdateLogic();
        }
    }

    private void LateUpdate()
    {
        // ������ ���� Ȥ�� �𸣴ϱ� LateUpdate�� ���� ���ֱ�
        if (curState != null)
        {
            curState.UpdateLate();
        }
    }

    // ���� �ٲٱ� ����� �� �Ʒ��� ���� �������� ���� ��
    // stateMachine.ChangeState(((BasicMonster)stateMachine).idleState);
    public void ChangeState(BaseState newState)
    {
        // State ������
        curState.Exit();

        // State ���� �Ҵ�
        curState = newState;
        curState.Enter();
    }

}
