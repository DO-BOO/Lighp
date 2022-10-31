using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : CharacterJump
{
    private void Update()
    {
        InputJump();
    }

    // Input���� �������� �����ϴ� �Լ�
    private void InputJump()
    {
        if (InputManager.GetKeyDown(InputAction.Jump))
        {
            Jump(move.MoveStat.jumpForce);
        }
    }
}
