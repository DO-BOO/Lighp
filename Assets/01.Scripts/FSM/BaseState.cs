using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// FSM�� State ���¸� �����س��� ��ũ��Ʈ
/// </summary>
public class BaseState
{
    // state �̸�
    public string name;
    protected StateMachine stateMachine;

    // State ������
    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    // ���µ�
    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdateLate() { }
    public virtual void Exit() { }

}
