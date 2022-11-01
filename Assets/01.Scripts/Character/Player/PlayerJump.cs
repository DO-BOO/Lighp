using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : CharacterJump
{
    protected override void ChildAwake()
    {
        EventManager<InputType>.StartListening((int)InputAction.Jump, InputJump);
    }

    // Input값을 바탕으로 점프하는 함수
    private void InputJump(InputType type)
    {
        if (type == InputType.GetKeyDown)
        {
            Jump(move.moveStat.jumpForce);
        }
    }

    private void OnDestroy()
    {
        EventManager<InputType>.StopListening((int)InputAction.Jump, InputJump);
    }
}
