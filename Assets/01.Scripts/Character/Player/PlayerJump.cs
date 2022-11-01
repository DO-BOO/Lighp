using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : CharacterJump
{
    protected override void ChildAwake()
    {
        EventManager.StartListening((int)InputAction.Jump, InputJump);
    }

    // Input���� �������� �����ϴ� �Լ�
    private void InputJump()
    {
        Debug.Log("Jump");
        Jump(move.MoveStat.jumpForce);
    }

    private void OnDestroy()
    {
        EventManager.StopListening((int)InputAction.Jump, InputJump);
    }
}
