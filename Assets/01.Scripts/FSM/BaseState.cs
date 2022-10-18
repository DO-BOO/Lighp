using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// FSM에 State 상태를 정의해놓은 스크립트
/// </summary>
public class BaseState
{
    // state 이름
    public string name;
    protected StateMachine stateMachine;

    // State 생성자
    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    // 상태들
    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdateLate() { }
    public virtual void Exit() { }

}
