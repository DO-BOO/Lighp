using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : CharacterJump
{
    private void Update()
    {
        InputJump();
    }

    // Input값을 바탕으로 점프하는 함수
    private void InputJump()
    {
        if (InputManager.GetKeyDown(InputAction.Jump))
        {
            Jump(move.MoveStat.jumpForce);
        }
    }
}
