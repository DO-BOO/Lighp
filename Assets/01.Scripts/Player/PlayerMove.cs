using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : CharacterMove
{
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        InputMove();
        InputJump();
    }

    private void InputMove()
    {
        Vector3 moveInput = Vector3.zero;
        moveInput.x = Input.GetAxisRaw(Define.HORIZONTAL);
        moveInput.z = Input.GetAxisRaw(Define.VERTICAL);

        Move(moveInput.normalized, moveStat.speed, true, moveStat.rotationSpeed);
    }

    private void InputJump()
    {
        if(Input.GetButtonDown(Define.JUMP))
        {
            Jump(moveStat.jumpForce);
        }
    }
}
